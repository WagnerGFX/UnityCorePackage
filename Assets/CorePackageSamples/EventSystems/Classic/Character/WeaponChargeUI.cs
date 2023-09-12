using CorePackage.EventSystems.Classic;
using CorePackageSamples.ClassicEvents.Events;
using UnityEngine;
using UnityEngine.UI;

namespace CorePackageSamples.ClassicEvents.Character
{
    public class WeaponChargeUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _chargeUI;

        [SerializeField]
        private float _chargeUIOffset = 72f;

        [Space]
        [SerializeField]
        private Image _chargeBar;

        private IEventManager _eventManager;
        private RectTransform _chargeRect;
        private Camera _camera;


        private void Awake()
        {
            _eventManager = GetComponent<IEventManager>();
            _chargeRect = GetComponent<RectTransform>();
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _eventManager.Subscribe<OnWeaponCharging>(UpdateWeaponCharge);
            _eventManager.Subscribe<OnCharacterMoved>(UpdateUIPositionToCharacter);
            _eventManager.Subscribe<OnCharacterRotated>(UpdateUIPositionToAim);
        }

        private void OnDisable()
        {
            _eventManager.Unsubscribe<OnWeaponCharging>(UpdateWeaponCharge);
            _eventManager.Unsubscribe<OnCharacterMoved>(UpdateUIPositionToCharacter);
            _eventManager.Unsubscribe<OnCharacterRotated>(UpdateUIPositionToAim);
        }

        private void UpdateWeaponCharge(OnWeaponCharging eArgs)
        {
            _chargeBar.fillAmount = eArgs.ChargedTimeNormalized;

            if (eArgs.IsReady)
            {
                _chargeUI.gameObject.SetActive(false);
            }
            else
            {
                _chargeUI.gameObject.SetActive(true);
            }
        }

        private void UpdateUIPositionToCharacter(OnCharacterMoved eArgs)
        {
            Vector3 screenPosition = _camera.WorldToScreenPoint(eArgs.CurrentPosition);
            _chargeRect.position = screenPosition;
        }

        private void UpdateUIPositionToAim(OnCharacterRotated eArgs)
        {
            if (eArgs.AimAngle > 180f)
            {
                _chargeUI.localPosition = Vector3.up * _chargeUIOffset;
            }
            else
            {
                _chargeUI.localPosition = Vector3.down * _chargeUIOffset;
            }
        }
    }
}
