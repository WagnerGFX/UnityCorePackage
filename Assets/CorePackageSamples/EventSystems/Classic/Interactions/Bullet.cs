using System.Collections;
using UnityEngine;

namespace Interactions
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

        private Renderer m_renderer;
        private Rigidbody2D m_rigidbody;
        private WaitForSeconds m_sleepForOneSec = new(1);
        private Coroutine m_destructionChecker;


        private void Awake()
        {
            m_renderer = GetComponent<Renderer>();
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_rigidbody.velocity = transform.right * Speed;

            Direction = m_rigidbody.velocity.normalized;

            m_destructionChecker = StartCoroutine(CheckForDestruction());
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(this);
            }


            StopCoroutine(m_destructionChecker);
            Destroy(gameObject);
        }

        private IEnumerator CheckForDestruction()
        {
            const int attemptsLimit = 10;

            for (int i = 0; i < attemptsLimit; i++)
            {
                yield return m_sleepForOneSec;

                if (!m_renderer.isVisible)
                {
                    Destroy(gameObject);
                    break;
                }
            }

            Destroy(gameObject);
        }
    }
}