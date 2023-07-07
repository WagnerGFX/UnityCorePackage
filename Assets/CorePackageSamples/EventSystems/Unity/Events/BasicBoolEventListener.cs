﻿using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace CorePackageSamples.UnityEvents
{
    /// <summary>
    /// Listener for events with one argument: boolean
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Basic Bool Listener", 18)]
    public class BasicBoolEventListener : BaseEventListener<BasicBoolEventChannelSO, bool>
    {

    }
}
