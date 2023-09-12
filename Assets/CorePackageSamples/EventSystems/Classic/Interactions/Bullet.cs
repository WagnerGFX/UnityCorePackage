using System.Collections;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Interactions
{
    public sealed class Bullet : MonoBehaviour, IDamager
    {
        [field: SerializeField]
        public float Damage { get; private set; }

        [field: SerializeField]
        public float Force { get; private set; }

        [field: SerializeField]
        [field: Range(0f, 50f)]
        [field: Tooltip("In units per second")]
        public float Speed { get; private set; } = 20f;

        public Vector2 Direction { get; private set; }

        private Renderer _renderer;
        private Rigidbody2D _rigidbody;
        private readonly WaitForSeconds _sleepForOneSec = new(1);
        private Coroutine _destructionChecker;


        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = transform.right * Speed;

            Direction = _rigidbody.velocity.normalized;

            _destructionChecker = StartCoroutine(CheckForDestruction());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(this);
                StopCoroutine(_destructionChecker);
                Destroy(gameObject);
            }
        }

        private IEnumerator CheckForDestruction()
        {
            const int attemptsLimit = 10;

            for (int i = 0; i < attemptsLimit; i++)
            {
                yield return _sleepForOneSec;

                if (!_renderer.isVisible)
                {
                    Destroy(gameObject);
                    break;
                }
            }

            Destroy(gameObject);
        }
    }
}
