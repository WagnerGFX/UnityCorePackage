﻿using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Listener for events with one argument: GameObject
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Basic GameObject Listener", 19)]
    public class BasicGameObjectEventListener : BaseEventListener<BasicGameObjectEventChannelSO, GameObject>
    {

    }
}
