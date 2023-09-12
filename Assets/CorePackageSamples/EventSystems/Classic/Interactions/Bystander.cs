using DG.Tweening;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Interactions
{
    public class Bystander : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private Color _flashingColor = Color.red;

        [SerializeField]
        [Range(0.01f, 5f)]
        private float _flashingTime = 0.1f;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void TakeDamage(IDamager damageSource)
        {
            _renderer
                .DOBlendableColor(_flashingColor, _flashingTime)
                .SetEase(Ease.InQuad)
                .SetLoops(2, LoopType.Yoyo);
        }
    }
}
