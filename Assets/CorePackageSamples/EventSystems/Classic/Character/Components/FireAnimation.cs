using CorePackage.EventSystems.Classic;
using CorePackageSamples.ClassicEvents.Events;
using DG.Tweening;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class FireAnimation : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private Rigidbody2D _turretRoot;

        private IEventManager _eventManager;

        private readonly float _maxRecoilTime = 0.2f;
        private readonly float _turretRecoilPositionX = 0.2f;
        private float _turretReadyPositionX;
        private float _recoverTime;
        private Transform _turretTransform;


        private void Awake()
        {
            DOTween.Init(recycleAllByDefault: false, useSafeMode: true, logBehaviour: LogBehaviour.Default);

            _eventManager = GetComponent<IEventManager>();
            _turretTransform = _turretRoot.transform;

            _turretReadyPositionX = _turretTransform.localPosition.x;
        }

        private void OnValidate()
        {
            this.AssertObjectField(_turretRoot, "Turret Root");
        }

        private void OnEnable()
        {
            _eventManager.Subscribe<OnWeaponFired>(AnimateTurretRecoil);
        }

        private void OnDisable()
        {
            _eventManager.Unsubscribe<OnWeaponFired>(AnimateTurretRecoil);
        }

        private void AnimateTurretRecoil(OnWeaponFired eArgs)
        {
            float m_recoilTime = Mathf.Min(_maxRecoilTime, eArgs.FireRate / 2f);
            _recoverTime = eArgs.FireRate - m_recoilTime;

            _turretTransform.DOLocalMoveX(_turretRecoilPositionX, m_recoilTime)
                .SetEase(Ease.OutQuad)
                .OnComplete(ReturnTurretToReadyPosition);
        }

        private void ReturnTurretToReadyPosition()
        {
            _turretTransform.DOLocalMoveX(_turretReadyPositionX, _recoverTime)
                .SetEase(Ease.InOutQuad);
        }
    }
}
