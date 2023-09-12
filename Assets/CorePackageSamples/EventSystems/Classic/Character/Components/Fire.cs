using CorePackage.EventSystems.Classic;
using CorePackageSamples.ClassicEvents.Events;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class Fire : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private Transform _firePosition;

        [SerializeField]
        private GameObject _bullet;

        [SerializeField]
        [Tooltip("Bullets per second")]
        [Range(0.1f, 30f)]
        private float _fireRate = 10f;

        private float _timer = 0f;
        private bool _shoot = false;
        private IEventManager _eventManager;


        private void Awake()
        {
            _eventManager = GetComponent<IEventManager>();

            _timer = 1 / _fireRate;
        }

        private void OnValidate()
        {
            this.AssertObjectField(_firePosition, "Fire Position");
            this.AssertObjectField(_bullet, "Bullet Prefab");
        }

        private void OnEnable()
        {
            _eventManager.Subscribe<OnInputFireTriggered>(OnFireTriggered);
        }

        private void OnDisable()
        {
            _eventManager.Unsubscribe<OnInputFireTriggered>(OnFireTriggered);
        }

        private void FixedUpdate()
        {
            float timeToNextShot = 1 / _fireRate;

            if (_timer < timeToNextShot)
            {
                _timer += Time.fixedDeltaTime;
            }

            if (_timer >= timeToNextShot && _shoot)
            {
                _timer %= timeToNextShot;

                Instantiate(_bullet, _firePosition.position, _firePosition.rotation);
                _eventManager.Invoke(new OnWeaponFired(timeToNextShot));
            }

            _eventManager.Invoke(new OnWeaponCharging(_fireRate, _timer / timeToNextShot));
        }

        private void OnFireTriggered(OnInputFireTriggered eArgs)
        {
            _shoot = eArgs.Pressed;
        }
    }
}
