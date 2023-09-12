using CorePackage.EventSystems.Classic;
using CorePackageSamples.ClassicEvents.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CorePackageSamples.ClassicEvents.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class Input : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private GameInputActionsSO _gameInput;

        private IEventManager _eventManager;


        private void Awake()
        {
            _eventManager = GetComponent<IEventManager>();

            _gameInput.PlayerActions.Enable();
        }

        private void OnEnable()
        {
            _gameInput.PlayerActions.Fire.performed += OnFire;
            _gameInput.PlayerActions.Fire.canceled += OnFire;
            _gameInput.PlayerActions.Move.performed += OnMove;
            _gameInput.PlayerActions.Move.canceled += OnMove;
            _gameInput.PlayerActions.Pointer.performed += OnPointerMove;
            _gameInput.PlayerActions.Exit.performed += OnExit;
        }

        private void OnDisable()
        {
            _gameInput.PlayerActions.Fire.performed -= OnFire;
            _gameInput.PlayerActions.Fire.canceled -= OnFire;
            _gameInput.PlayerActions.Move.performed -= OnMove;
            _gameInput.PlayerActions.Move.canceled -= OnMove;
            _gameInput.PlayerActions.Pointer.performed += OnPointerMove;
            _gameInput.PlayerActions.Exit.performed -= OnExit;
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            _eventManager.Invoke(new OnInputFireTriggered(context.performed));
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _eventManager.Invoke(new OnInputMoveChanged(context.ReadValue<Vector2>()));
        }

        private void OnPointerMove(InputAction.CallbackContext context)
        {
            _eventManager.Invoke(new OnInputPointerChanged(context.ReadValue<Vector2>()));
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
