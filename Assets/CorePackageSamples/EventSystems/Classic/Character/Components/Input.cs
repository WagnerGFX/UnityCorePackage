using CorePackageSamples.ClassicEvents.Events;
using CorePackage.EventSystems.Classic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CorePackageSamples.ClassicEvents.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class Input : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private GameInputActionsSO m_gameInput;

        private IEventManager m_eventManager;


        private void Awake()
        {
            m_eventManager = GetComponent<IEventManager>();

            m_gameInput.PlayerActions.Enable();
        }

        private void OnEnable()
        {
            m_gameInput.PlayerActions.Fire.performed += OnFire;
            m_gameInput.PlayerActions.Fire.canceled += OnFire;
            m_gameInput.PlayerActions.Move.performed += OnMove;
            m_gameInput.PlayerActions.Move.canceled += OnMove;
            m_gameInput.PlayerActions.Pointer.performed += OnPointerMove;
            m_gameInput.PlayerActions.Exit.performed += OnExit;
        }

        private void OnDisable()
        {
            m_gameInput.PlayerActions.Fire.performed -= OnFire;
            m_gameInput.PlayerActions.Fire.canceled -= OnFire;
            m_gameInput.PlayerActions.Move.performed -= OnMove;
            m_gameInput.PlayerActions.Move.canceled -= OnMove;
            m_gameInput.PlayerActions.Pointer.performed += OnPointerMove;
            m_gameInput.PlayerActions.Exit.performed -= OnExit;
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            m_eventManager.Invoke(new OnInputFireTriggered(context.performed));
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            m_eventManager.Invoke(new OnInputMoveChanged(context.ReadValue<Vector2>()));
        }

        private void OnPointerMove(InputAction.CallbackContext context)
        {
            m_eventManager.Invoke(new OnInputPointerChanged(context.ReadValue<Vector2>()));
        }

        private void OnExit(InputAction.CallbackContext context)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}