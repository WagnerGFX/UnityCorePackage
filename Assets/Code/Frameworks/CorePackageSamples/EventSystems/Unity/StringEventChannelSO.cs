﻿using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// This class is used for Events that have one string argument.
    /// Example: An Achievement unlock event, where the int is the Achievement ID.
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/String Event", order = 1)]
    public class StringEventChannelSO : BaseEventChannelSO<string, object> { } 
}