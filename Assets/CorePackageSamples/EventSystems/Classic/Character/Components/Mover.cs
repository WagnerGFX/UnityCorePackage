using CorePackage.EventSystems.Classic;
using CorePackageSamples.ClassicEvents.Events;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class Mover : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        [Tooltip("Speed in units per second.")]
        private float _movementSpeed = 1f;

        private IEventManager _eventManager;
        private Vector2 _previousPosition;


        private void Awake()
        {
            _eventManager = GetComponent<IEventManager>();
        }

        private void OnValidate()
        {
            this.AssertObjectField(_rigidbody, "Rigidbody");
        }

        private void OnEnable()
        {
            _eventManager.Subscribe<OnInputMoveChanged>(OnMoveChanged);
        }

        private void OnDisable()
        {
            _eventManager.Unsubscribe<OnInputMoveChanged>(OnMoveChanged);
        }

        private void LateUpdate()
        {
            if (_rigidbody.position != _previousPosition)
            {
                _eventManager.Invoke(new OnCharacterMoved(_rigidbody.position, _rigidbody.velocity.normalized, _rigidbody.velocity.magnitude));
                _previousPosition = _rigidbody.position;
            }
        }

        private void OnMoveChanged(OnInputMoveChanged eArgs)
        {
            _rigidbody.velocity = _movementSpeed * eArgs.MoveDirection;
        }
    }
}
