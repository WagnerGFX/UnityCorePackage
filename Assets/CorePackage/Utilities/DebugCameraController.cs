using UnityEngine;

namespace CorePackage.Utilities
{
    [AddComponentMenu("CorePackage/Debug Camera Controller", 0)]
    public class DebugCameraController : MonoBehaviour
    {
        [SerializeField]
        private Camera _myCamera;

        [Header("Movement")]
        [SerializeField]
        [Tooltip("Movement speed.")]
        private float _speedPower = 3.5f;

        [SerializeField]
        [Tooltip("Movement boost.")]
        private float _speedBoost = 10f;

        [SerializeField]
        [Tooltip("Makes movement snappier or floaty."), Range(0.001f, 1f)]
        private float _movementSmoothness = 0.2f;

        [SerializeField]
        [Header("Look Around")]

        [Tooltip("Speed factor for camera Zoom or FoV changes.")]
        private float _zoomSpeed = 5f;

        [SerializeField]
        [Tooltip("Modify mouse speed factor to allow more precision in smaller movements and higher speed in faster movements.\nX = Mouse speed.\nY = Multiplicative factor for mouse speed.")]
        private AnimationCurve _mouseSensitivityCurve = new(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        [SerializeField]
        [Tooltip("Makes looking around snappier or smoother."), Range(0.001f, 1f)]
        private float _cameraSmoothness = 0.01f;

        [SerializeField]
        [Tooltip("Invert Y axis for mouse input.")]
        private bool _invertCameraYAxis = false;

        [SerializeField]
        [Tooltip("Use the mouse to control the camera's rotation in orthograpic mode.")]
        private bool _rotateInOrthograpic = false;

        private readonly CameraState _targetCameraState = new();
        private readonly CameraState _interpolatingCameraState = new();
        private readonly GUIStyle _style = new();
        private Transform _cameraTransform;
        private bool _isActive = false;
        private bool _showGui = true;
        private bool _enableLookAround = false;
        private bool _enableMoveAround = false;
        private bool _enableBoost = false;
        private const float SPEED_CHANGE_FACTOR = 0.02f;
        private const KeyCode KEY_TOGGLE_CAMERA = KeyCode.Space;
        private const KeyCode KEY_TOGGLE_GUI = KeyCode.Alpha1;
        private const KeyCode KEY_TOGGLE_PROJECTION = KeyCode.Alpha2;
        private const KeyCode KEY_TOGGLE_ORTHO_ROTATION = KeyCode.Alpha3;
        private const KeyCode KEY_QUIT = KeyCode.Escape;
        private const KeyCode KEY_CAMERA_LOOK = KeyCode.Mouse1;
        private const KeyCode KEY_CAMERA_MOVE = KeyCode.Mouse2;
        private const KeyCode KEY_ZOOM_UP = KeyCode.Z;
        private const KeyCode KEY_ZOOM_DOWN = KeyCode.X;
        private const KeyCode KEY_SPEED_BOOST = KeyCode.LeftShift;
        private const KeyCode KEY_MOVE_UP = KeyCode.E;
        private const KeyCode KEY_MOVE_DOWN = KeyCode.Q;
        private const KeyCode KEY_MOVE_FORWARD = KeyCode.W;
        private const KeyCode KEY_MOVE_BACK = KeyCode.S;
        private const KeyCode KEY_MOVE_LEFT = KeyCode.A;
        private const KeyCode KEY_MOVE_RIGHT = KeyCode.D;


        private void OnEnable()
        {
            _cameraTransform = _myCamera.transform;

            _targetCameraState.SetFromTransform(_cameraTransform);
            _interpolatingCameraState.SetFromTransform(_cameraTransform);

            _style.normal.textColor = Color.white;
            _style.fontSize = 20;
        }

        private void OnValidate()
        {
            this.AssertObjectField(_myCamera, nameof(_myCamera));
        }

        private void Update()
        {
            CheckInputSettings();

            if (!_isActive)
            { return; }

            CameraZoom();
            CameraLookRotation();
            CameraMouseMovement();
            CameraMovement();
            CameraSmooth();
        }

        private void OnGUI()
        {
            if (!_showGui)
            { return; }

            const float lineHeight = 32;
            const float margin = 5;
            float nextPos = margin;

            WriteShortcut("ESC", "Quit");
            WriteShortcut("SPACE", "Toggle Camera Controller");
            WriteShortcut("1", "Toggle GUI");

            if (_isActive)
            {
                WriteShortcut("2", "Toggle Projection Mode");
                WriteShortcut("3", "Toggle Mouse in Orthographic");
                WriteShortcut("RMB", "Hold to Look Around");
                WriteShortcut("MMB", "Hold to Move Around");
                WriteShortcut("WASD QE", "Movement");
                WriteShortcut("LSHIFT", "Speed Boost");
                WriteShortcut("SCROLL", "Speed +/-");
                WriteShortcut("ZX", "Zoom +/-");
            }

            void WriteShortcut(string KeyName, string description)
            {
                GUI.skin.label.alignment = TextAnchor.MiddleRight;
                GUI.Label(new Rect(margin, nextPos, 100, lineHeight), new GUIContent(KeyName), _style);

                GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                GUI.Label(new Rect(margin + 110f, nextPos, 500, lineHeight), new GUIContent("| " + description), _style);

                nextPos += lineHeight;
            }
        }


        private void CheckInputSettings()
        {
            //Quit
            if (Input.GetKey(KEY_QUIT))
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }

            // Camera
            if (Input.GetKeyDown(KEY_TOGGLE_CAMERA))
            {
                _isActive = !_isActive;
            }

            // GUI
            if (Input.GetKeyDown(KEY_TOGGLE_GUI))
            {
                _showGui = !_showGui;
            }

            // Look Around
            if (_isActive && !_enableMoveAround && Input.GetKey(KEY_CAMERA_LOOK))
            {
                _enableLookAround = !_myCamera.orthographic || (_myCamera.orthographic && _rotateInOrthograpic);
            }
            else
            {
                _enableLookAround = false;
            }

            //Move Around
            _enableMoveAround = _isActive && !_enableLookAround && Input.GetKey(KEY_CAMERA_MOVE);

            // Projection Mode
            if (_isActive && Input.GetKeyDown(KEY_TOGGLE_PROJECTION))
            {
                _myCamera.orthographic = !_myCamera.orthographic;
                Debug.LogFormat("DebugCamera: {0} Projection", _myCamera.orthographic ? "Orthographic" : "Perspective");
            }

            // Rotate in Orthographic
            if (_isActive && Input.GetKeyDown(KEY_TOGGLE_ORTHO_ROTATION))
            {
                _rotateInOrthograpic = !_rotateInOrthograpic;
                Debug.LogFormat("DebugCamera: Orthographic Mouse {0}", _rotateInOrthograpic ? "Enabled" : "Disabled");
            }

            // Boost
            _enableBoost = _isActive && Input.GetKey(KEY_SPEED_BOOST);

            // Speed change
            if (_isActive)
            {
                _speedPower += Input.mouseScrollDelta.y * SPEED_CHANGE_FACTOR;
            }

            MouseDisplay();
        }

        private void CameraZoom()
        {
            if (_myCamera.orthographic)
            {
                if (Input.GetKey(KEY_ZOOM_UP))
                { _myCamera.orthographicSize -= _zoomSpeed * Time.deltaTime; }

                if (Input.GetKey(KEY_ZOOM_DOWN))
                { _myCamera.orthographicSize += _zoomSpeed * Time.deltaTime; }
            }
            else
            {
                if (Input.GetKey(KEY_ZOOM_UP))
                { _myCamera.fieldOfView = Mathf.Clamp(_myCamera.fieldOfView - (_zoomSpeed * Time.deltaTime), 5, 160); }
                if (Input.GetKey(KEY_ZOOM_DOWN))
                { _myCamera.fieldOfView = Mathf.Clamp(_myCamera.fieldOfView + (_zoomSpeed * Time.deltaTime), 5, 160); }
            }
        }

        private void CameraLookRotation()
        {
            if (!_enableLookAround)
            { return; }

            Vector2 mouseMovement = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (_invertCameraYAxis ? 1f : -1f));

            float mouseSensitivityFactor = _mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

            _targetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
            _targetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;
        }

        private void CameraMouseMovement()
        {
            if (!_enableMoveAround)
            { return; }

            Vector2 mouseMovement = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (_invertCameraYAxis ? -1f : 1f));

            // Invert and lower the sensitivity
            mouseMovement *= -0.05f;

            float mouseSensitivityFactor = _mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);
            mouseMovement *= mouseSensitivityFactor;

            if (_enableBoost)
            { mouseMovement *= _speedBoost; }

            mouseMovement *= Mathf.Pow(2.0f, _speedPower);

            _targetCameraState.Translate(mouseMovement);
        }

        private void CameraMovement()
        {
            Vector3 movement;

            if (_myCamera.orthographic)
            { movement = GetInputTranslationDirection_2D() * Time.deltaTime; }
            else
            { movement = GetInputTranslationDirection_3D() * Time.deltaTime; }

            if (_enableBoost)
            { movement *= _speedBoost; }


            movement *= Mathf.Pow(2.0f, _speedPower);

            _targetCameraState.Translate(movement);
        }

        private void CameraSmooth()
        {
            // Framerate-independent interpolation
            // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
            float positionLerpPct = 1f - Mathf.Exp(Mathf.Log(1f - 0.99f) / _movementSmoothness * Time.deltaTime);
            float rotationLerpPct = 1f - Mathf.Exp(Mathf.Log(1f - 0.99f) / _cameraSmoothness * Time.deltaTime);
            _interpolatingCameraState.LerpTowards(_targetCameraState, positionLerpPct, rotationLerpPct);

            _interpolatingCameraState.UpdateTransform(_cameraTransform);
        }

        private void MouseDisplay()
        {
            bool hideMouse = _isActive && (_enableLookAround || _enableMoveAround);

            Cursor.visible = !hideMouse;

            if (hideMouse)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private Vector3 GetInputTranslationDirection_3D()
        {
            Vector3 direction = Vector3.zero;
            if (Input.GetKey(KEY_MOVE_DOWN))
            {
                direction += Vector3.down;
            }
            if (Input.GetKey(KEY_MOVE_UP))
            {
                direction += Vector3.up;
            }

            if (Input.GetKey(KEY_MOVE_FORWARD))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey(KEY_MOVE_BACK))
            {
                direction += Vector3.back;
            }

            if (Input.GetKey(KEY_MOVE_LEFT))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KEY_MOVE_RIGHT))
            {
                direction += Vector3.right;
            }

            return direction;
        }

        private Vector3 GetInputTranslationDirection_2D()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KEY_MOVE_DOWN))
            {
                direction += Vector3.back;
            }
            if (Input.GetKey(KEY_MOVE_UP))
            {
                direction += Vector3.forward;
            }

            if (Input.GetKey(KEY_MOVE_FORWARD))
            {
                direction += Vector3.up;
            }
            if (Input.GetKey(KEY_MOVE_BACK))
            {
                direction += Vector3.down;
            }

            if (Input.GetKey(KEY_MOVE_LEFT))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KEY_MOVE_RIGHT))
            {
                direction += Vector3.right;
            }

            return direction;
        }

        private class CameraState
        {
            public float yaw;
            public float pitch;
            public float roll;
            public float x;
            public float y;
            public float z;

            public void SetFromTransform(Transform t)
            {
                pitch = t.eulerAngles.x;
                yaw = t.eulerAngles.y;
                roll = t.eulerAngles.z;
                x = t.position.x;
                y = t.position.y;
                z = t.position.z;
            }

            public void Translate(Vector3 translation)
            {
                Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

                x += rotatedTranslation.x;
                y += rotatedTranslation.y;
                z += rotatedTranslation.z;
            }

            public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
            {
                yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
                pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
                roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);

                x = Mathf.Lerp(x, target.x, positionLerpPct);
                y = Mathf.Lerp(y, target.y, positionLerpPct);
                z = Mathf.Lerp(z, target.z, positionLerpPct);
            }

            public void UpdateTransform(Transform t)
            {
                t.eulerAngles = new Vector3(pitch, yaw, roll);
                t.position = new Vector3(x, y, z);
            }
        }
    }
}
