﻿using UnityEngine;
using CorePackage.Common;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Int", 1)]
    public class IntEventListener : BaseEventListener<IntEventChannelSO, int, object>
    {

    }
}