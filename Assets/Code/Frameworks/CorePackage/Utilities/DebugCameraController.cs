using UnityEngine;
using CorePackage.Common;

namespace CorePackage.Utilities
{
    [AddComponentMenu(Project.MenuName + "/Debug Camera Controller", 0)]
    public class DebugCameraController : MonoBehaviour
    {
        [SerializeField]
        private Camera myCamera;

        [Header("Movement")]
        [SerializeField]
        [Tooltip("Movement speed.")]
        private float speedPower = 3.5f;

        [SerializeField]
        [Tooltip("Movement boost.")]
        private float speedBoost = 10f;

        [SerializeField]
        [Tooltip("Makes movement snappier or floaty."), Range(0.001f, 1f)]
        private float movementSmoothness = 0.2f;

        [SerializeField]
        [Header("Look Around")]

        [Tooltip("Speed factor for camera Zoom or FoV changes.")]
        private float zoomSpeed = 5f;
        
        [SerializeField]
        [Tooltip("Modify mouse speed factor to allow more precision in smaller movements and higher speed in faster movements.\nX = Mouse speed.\nY = Multiplicative factor for mouse speed.")]
        private AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        [SerializeField]
        [Tooltip("Makes looking around snappier or smoother."), Range(0.001f, 1f)]
        private float cameraSmoothness = 0.01f;

        [SerializeField]
        [Tooltip("Invert Y axis for mouse input.")]
        private bool invertCameraYAxis = false;

        [SerializeField]
        [Tooltip("Use the mouse to control the camera's rotation in orthograpic mode.")]
        private bool rotateInOrthograpic = false;

        private readonly CameraState m_TargetCameraState = new();
        private readonly CameraState m_InterpolatingCameraState = new();
        private readonly GUIStyle style = new();
        private Transform cameraTransform;
        private bool isActive = false;
        private bool showGui = true;
        private bool enableLookAround = false;
        private bool enableMoveAround = false;
        private bool enableBoost = false;
        private readonly float speedChangeFactor = 0.02f;
        

        const KeyCode KEY_TOGGLE_CAMERA = KeyCode.Space;
        const KeyCode KEY_TOGGLE_GUI = KeyCode.Alpha1;
        const KeyCode KEY_TOGGLE_PROJECTION = KeyCode.Alpha2;
        const KeyCode KEY_TOGGLE_ORTHO_ROTATION = KeyCode.Alpha3;
        const KeyCode KEY_QUIT = KeyCode.Escape;
        const KeyCode KEY_CAMERA_LOOK = KeyCode.Mouse1;
        const KeyCode KEY_CAMERA_MOVE = KeyCode.Mouse2;
        const KeyCode KEY_ZOOM_UP = KeyCode.Z;
        const KeyCode KEY_ZOOM_DOWN = KeyCode.X;
        const KeyCode KEY_SPEED_BOOST = KeyCode.LeftShift;
        const KeyCode KEY_MOVE_UP = KeyCode.E;
        const KeyCode KEY_MOVE_DOWN = KeyCode.Q;
        const KeyCode KEY_MOVE_FORWARD = KeyCode.W;
        const KeyCode KEY_MOVE_BACK = KeyCode.S;
        const KeyCode KEY_MOVE_LEFT = KeyCode.A;
        const KeyCode KEY_MOVE_RIGHT = KeyCode.D;


        private void OnEnable()
        {
            cameraTransform = myCamera.transform;

            m_TargetCameraState.SetFromTransform(cameraTransform);
            m_InterpolatingCameraState.SetFromTransform(cameraTransform);

            style.normal.textColor = Color.white;
            style.fontSize = 20;
        }

        private void OnValidate()
        {
            this.AssertObjectField(myCamera, nameof(myCamera));
        }

        private void Update()
        {
            CheckInputSettings();

            if (!isActive)
            { return; }
            
            CameraZoom();
            CameraLookRotation();
            CameraMouseMovement();
            CameraMovement();
            CameraSmooth();
        }

        private void OnGUI()
        {
            if (!showGui)
            { return; }

            const float lineHeight = 32;
            const float margin = 5;
            float nextPos = margin;
            
            WriteShortcut("ESC", "Quit");
            WriteShortcut("SPACE", "Toggle Camera Controller");
            WriteShortcut("1", "Toggle GUI");

            if (isActive)
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
                GUI.Label(new Rect(margin, nextPos, 100, lineHeight), new GUIContent(KeyName), style);

                GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                GUI.Label(new Rect(margin + 110f, nextPos, 500, lineHeight), new GUIContent("| " + description), style);

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
                isActive = !isActive;
            }

            // GUI
            if (Input.GetKeyDown(KEY_TOGGLE_GUI))
            {
                showGui = !showGui;
            }

            // Look Around
            if (isActive && !enableMoveAround && Input.GetKey(KEY_CAMERA_LOOK))
            {
                enableLookAround = !myCamera.orthographic || (myCamera.orthographic && rotateInOrthograpic);
            }
            else
            {
                enableLookAround = false;
            }

            //Move Around
            enableMoveAround = (isActive && !enableLookAround && Input.GetKey(KEY_CAMERA_MOVE));

            // Projection Mode
            if (isActive && Input.GetKeyDown(KEY_TOGGLE_PROJECTION))
            {
                myCamera.orthographic = !myCamera.orthographic;
                Debug.LogFormat("DebugCamera: {0} Projection", (myCamera.orthographic ? "Orthographic" : "Perspective"));
            }

            // Rotate in Orthographic
            if (isActive && Input.GetKeyDown(KEY_TOGGLE_ORTHO_ROTATION))
            {
                rotateInOrthograpic = !rotateInOrthograpic;
                Debug.LogFormat("DebugCamera: Orthographic Mouse {0}", (rotateInOrthograpic ? "Enabled" : "Disabled"));
            }

            // Boost
            enableBoost = isActive && Input.GetKey(KEY_SPEED_BOOST);

            // Speed change
            if (isActive)
            {
                speedPower += Input.mouseScrollDelta.y * speedChangeFactor;
            }

            MouseDisplay();
        }

        private void CameraZoom()
        {
            if (myCamera.orthographic)
            {
                if (Input.GetKey(KEY_ZOOM_UP))
                    myCamera.orthographicSize -= zoomSpeed * Time.deltaTime;
                if (Input.GetKey(KEY_ZOOM_DOWN))
                    myCamera.orthographicSize += zoomSpeed * Time.deltaTime;
            }
            else
            {
                if (Input.GetKey(KEY_ZOOM_UP))
                    myCamera.fieldOfView = Mathf.Clamp(myCamera.fieldOfView - zoomSpeed * Time.deltaTime, 5, 160);
                if (Input.GetKey(KEY_ZOOM_DOWN))
                    myCamera.fieldOfView = Mathf.Clamp(myCamera.fieldOfView + zoomSpeed * Time.deltaTime, 5, 160);
            }
        }

        private void CameraLookRotation()
        {
            if (!enableLookAround)
            { return; }

            var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertCameraYAxis ? 1f : -1f));

            var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

            m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
            m_TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;
        }

        private void CameraMouseMovement()
        {
            if (!enableMoveAround)
            { return; }

            var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertCameraYAxis ? -1f : 1f));
            
            // Invert and lower the sensitivity
            mouseMovement *= -0.05f;

            var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);
            mouseMovement *= mouseSensitivityFactor;

            if (enableBoost)
                mouseMovement *= speedBoost;

            mouseMovement *= Mathf.Pow(2.0f, speedPower);

            m_TargetCameraState.Translate(mouseMovement);
        }

        private void CameraMovement()
        {
            Vector3 movement;

            if (myCamera.orthographic)
                movement = GetInputTranslationDirection_2D() * Time.deltaTime;
            else
                movement = GetInputTranslationDirection_3D() * Time.deltaTime;

            if (enableBoost)
                movement *= speedBoost;

            
            movement *= Mathf.Pow(2.0f, speedPower);

            m_TargetCameraState.Translate(movement);
        }

        private void CameraSmooth()
        {
            // Framerate-independent interpolation
            // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
            var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / movementSmoothness) * Time.deltaTime);
            var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / cameraSmoothness) * Time.deltaTime);
            m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

            m_InterpolatingCameraState.UpdateTransform(cameraTransform);
        }

        private void MouseDisplay()
        {
            bool hideMouse = isActive && (enableLookAround || enableMoveAround);

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


        Vector3 GetInputTranslationDirection_3D()
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
        Vector3 GetInputTranslationDirection_2D()
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


        class CameraState
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