﻿using CorePackage.EventSystems.Unity;
using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Event SO that allows one argument: int
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Basic Int Event", order = 18)]
    public class BasicIntEventChannelSO : BaseEventChannelSO<int>
    { }
}
