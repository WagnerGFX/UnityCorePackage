using Character.Events;
using CorePackage.EventSystems.Classic;
using DG.Tweening;
using UnityEngine;

namespace Character.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class FireAnimation : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private Rigidbody2D m_turretRoot;

        private IEventManager m_eventManager;

        private readonly float c_maxRecoilTime = 0.2f;
        private readonly float c_turretRecoilPositionX = 0.2f;
        private float m_turretReadyPositionX;
        private float m_recoverTime;
        private Transform m_turretTransform;


        private void Awake()
        {
            DOTween.Init(recycleAllByDefault: false, useSafeMode: true, logBehaviour: LogBehaviour.Default);

            m_eventManager = GetComponent<IEventManager>();
            m_turretTransform = m_turretRoot.transform;

            m_turretReadyPositionX = m_turretTransform.localPosition.x;
        }

        private void OnValidate()
        {
            this.AssertObjectField(m_turretRoot, "Turret Root");
        }

        private void OnEnable()
        {
            m_eventManager.Subscribe<OnWeaponFired>(AnimateTurretRecoil);
        }

        private void OnDisable()
        {
            m_eventManager.Unsubscribe<OnWeaponFired>(AnimateTurretRecoil);
        }

        private void AnimateTurretRecoil(OnWeaponFired eArgs)
        {
            float m_recoilTime = Mathf.Min(c_maxRecoilTime, eArgs.FireRate / 2f);
            m_recoverTime = eArgs.FireRate - m_recoilTime;

            m_turretTransform.DOLocalMoveX(c_turretRecoilPositionX, m_recoilTime)
                .SetEase(Ease.OutQuad)
                .OnComplete(ReturnTurretToReadyPosition);
        }

        private void ReturnTurretToReadyPosition()
        {
            m_turretTransform.DOLocalMoveX(m_turretReadyPositionX, m_recoverTime)
                .SetEase(Ease.InOutQuad);
        }
    }
}
