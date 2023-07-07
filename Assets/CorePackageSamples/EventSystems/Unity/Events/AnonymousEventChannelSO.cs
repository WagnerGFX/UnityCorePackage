﻿using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO with no arguments.
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Anonymous Event", order = 21)]
    public class AnonymousEventChannelSO : BaseEventChannelSO { } 
}
