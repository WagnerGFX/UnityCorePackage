using UnityEngine;

namespace CorePackage.Utilities
{
    [AddComponentMenu("MyProjectName/Debug Camera Controller", 0)]
    [RequireComponent(typeof(Camera))]
    public class DebugCameraController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField]
        [Tooltip("Speed factor on camera Zoom or FoV, controllable by Z and X.")]
        private float zoomSpeed = 5f;

        [SerializeField]
        [Tooltip("Exponential boost factor on translation, controllable by mouse wheel.")]
        private float boost = 3.5f;

        [SerializeField]
        [Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
        private float positionLerpTime = 0.2f;

        [SerializeField]
        [Header("Rotation Settings")]
        [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
        private AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        [SerializeField]
        [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
        private float rotationLerpTime = 0.01f;

        [SerializeField]
        [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
        private bool invertY = false;

        [SerializeField]
        [Tooltip("Whether or not to rotate the camera when in orthograpic mode.")]
        private bool rotateInOrthograpic = false;

        private CameraState m_TargetCameraState = new CameraState();
        private CameraState m_InterpolatingCameraState = new CameraState();
        private Camera myCamera;
        private bool isActive = false;


        private void OnEnable()
        {
            myCamera = GetComponent<Camera>();

            m_TargetCameraState.SetFromTransform(transform);
            m_InterpolatingCameraState.SetFromTransform(transform);

            Debug.Log("DebugCamera is Enabled. Hold RMB to use.\nWASDQE for movement | ZX for zoom | C to switch projection | R to rotate in orthographic");
        }

        private void Update()
        {
            CheckQuitCommand();

            CheckActivation();

            if (!isActive)
                return;

            CameraZoom();
            CameraRotation();
            CameraTranslation();
            CameraSmooth();
            CameraSettings();
        }

        private void CheckQuitCommand()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

        private void CheckActivation()
        {
            // Hide and lock cursor when right mouse button pressed
            if (Input.GetMouseButtonDown(1))
            {
                isActive = true;
                Cursor.lockState = CursorLockMode.Locked;

            }

            // Unlock and show cursor when right mouse button released
            if (Input.GetMouseButtonUp(1))
            {
                isActive = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void CameraSettings()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                myCamera.orthographic = !myCamera.orthographic;
                Debug.LogFormat("DebugCamera: {0} Projection", (myCamera.orthographic ? "Orthographic" : "Perspective"));
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                rotateInOrthograpic = !rotateInOrthograpic;
                Debug.LogFormat("DebugCamera: Rotation {0}", (rotateInOrthograpic ? "Enabled" : "Disabled"));
            }
        }

        private void CameraZoom()
        {
            if (myCamera.orthographic)
            {
                if (Input.GetKey(KeyCode.Z))
                    myCamera.orthographicSize += zoomSpeed * Time.deltaTime;
                if (Input.GetKey(KeyCode.X))
                    myCamera.orthographicSize -= zoomSpeed * Time.deltaTime;
            }
            else
            {
                if (Input.GetKey(KeyCode.Z))
                    myCamera.fieldOfView = Mathf.Clamp(myCamera.fieldOfView + zoomSpeed * Time.deltaTime, 5, 160);
                if (Input.GetKey(KeyCode.X))
                    myCamera.fieldOfView = Mathf.Clamp(myCamera.fieldOfView - zoomSpeed * Time.deltaTime, 5, 160);
            }
        }

        private void CameraRotation()
        {
            if (myCamera.orthographic && !rotateInOrthograpic)
                return;

            var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

            var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

            m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
            m_TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;
        }

        private void CameraTranslation()
        {
            Vector3 translation;

            if (myCamera.orthographic)
                translation = GetInputTranslationDirection_2D() * Time.deltaTime;
            else
                translation = GetInputTranslationDirection_3D() * Time.deltaTime;

            // Speed up movement when shift key held
            if (Input.GetKey(KeyCode.LeftShift))
                translation *= 10.0f;

            // Modify movement by a boost factor (defined in Inspector and modified in play mode through the mouse scroll wheel)
            boost += Input.mouseScrollDelta.y * 0.2f;
            translation *= Mathf.Pow(2.0f, boost);

            m_TargetCameraState.Translate(translation);
        }

        private void CameraSmooth()
        {
            // Framerate-independent interpolation
            // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
            var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
            var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
            m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

            m_InterpolatingCameraState.UpdateTransform(transform);
        }


        Vector3 GetInputTranslationDirection_3D()
        {
            Vector3 direction = Vector3.zero;
            if (Input.GetKey(KeyCode.Q))
            {
                direction += Vector3.down;
            }
            if (Input.GetKey(KeyCode.E))
            {
                direction += Vector3.up;
            }

            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.back;
            }

            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }

            return direction;
        }
        Vector3 GetInputTranslationDirection_2D()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.Q))
            {
                direction += Vector3.back;
            }
            if (Input.GetKey(KeyCode.E))
            {
                direction += Vector3.forward;
            }

            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.down;
            }

            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
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