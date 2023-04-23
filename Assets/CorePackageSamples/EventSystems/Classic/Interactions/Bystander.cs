using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class Bystander : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private Color m_flashingColor = Color.red;

        [SerializeField]
        [Range(0.01f, 5f)]
        private float m_flashingTime = 0.1f;

        private SpriteRenderer m_renderer;

        void Awake()
        {
            m_renderer = GetComponent<SpriteRenderer>();
        }

        public void TakeDamage(IDamager damageSource)
        {
            m_renderer.DOBlendableColor(m_flashingColor, m_flashingTime)
                .SetEase(Ease.InQuad)
                .SetLoops(2, LoopType.Yoyo);
        }
    } 
}
