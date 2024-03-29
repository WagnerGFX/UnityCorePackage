﻿using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Event SO that allows one argument: string
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Basic String Event", order = 18)]
    public class BasicStringEventChannelSO : BaseEventChannelSO<string>
    { }
}
