using CorePackageSamples.ClassicEvents.Events;
using CorePackage.EventSystems.Classic;
using UnityEngine;
using UnityEngine.UI;

namespace CorePackageSamples.ClassicEvents.Character
{
    public class WeaponChargeUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_chargeUI;

        [SerializeField]
        private float m_chargeUIOffset = 72f;
        
        [Space]
        [SerializeField]
        private Image m_chargeBar;

        private IEventManager m_eventManager;
        private RectTransform m_chargeRect;
        private Camera m_camera;


        private void Awake()
        {
            m_eventManager = GetComponent<IEventManager>();
            m_chargeRect = GetComponent<RectTransform>();
            m_camera = Camera.main;
        }

        private void OnEnable()
        {
            m_eventManager.Subscribe<OnWeaponCharging>(UpdateWeaponCharge);
            m_eventManager.Subscribe<OnCharacterMoved>(UpdateUIPositionToCharacter);
            m_eventManager.Subscribe<OnCharacterRotated>(UpdateUIPositionToAim);
        }

        private void OnDisable()
        {
            m_eventManager.Unsubscribe<OnWeaponCharging>(UpdateWeaponCharge);
            m_eventManager.Unsubscribe<OnCharacterMoved>(UpdateUIPositionToCharacter);
            m_eventManager.Unsubscribe<OnCharacterRotated>(UpdateUIPositionToAim);
        }

        private void UpdateWeaponCharge(OnWeaponCharging eArgs)
        {
            m_chargeBar.fillAmount = eArgs.ChargedTimeNormalized;

            if (eArgs.IsReady)
            {
                m_chargeUI.gameObject.SetActive(false);
            }
            else
            {
                m_chargeUI.gameObject.SetActive(true);
            }
        }

        private void UpdateUIPositionToCharacter(OnCharacterMoved eArgs)
        {
            Vector3 screenPosition = m_camera.WorldToScreenPoint(eArgs.CurrentPosition);
            m_chargeRect.position = screenPosition;
        }

        private void UpdateUIPositionToAim(OnCharacterRotated eArgs)
        {
            if(eArgs.AimAngle > 180f)
            {
                m_chargeUI.localPosition = Vector3.up * m_chargeUIOffset;
            }
            else
            {
                m_chargeUI.localPosition = Vector3.down * m_chargeUIOffset;
            }

        }
    }
}
