using Character.Events;
using CorePackage.EventSystems.Classic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private IEventManager m_eventManager;

    private void Awake()
    {
        m_eventManager = GetComponent<IEventManager>();
    }

    private void OnValidate()
    {
        this.AssertObjectField(_audioSource, "Audio Source");
    }

    private void OnEnable()
    {
        m_eventManager.Subscribe<OnWeaponFired>(PlayWeaponSound);
    }

    private void OnDisable()
    {
        m_eventManager.Unsubscribe<OnWeaponFired>(PlayWeaponSound);
    }

    private void PlayWeaponSound(OnWeaponFired eArgs)
    {
        float nextPitch = Random.Range(0.9f, 1.11f);
        _audioSource.pitch = nextPitch;
        _audioSource.Play();
    }
}
