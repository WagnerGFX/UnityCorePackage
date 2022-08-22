﻿using UnityEngine;
using CorePackage.Common;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Float", 1)]
    public class FloatEventListener : BaseEventListener<FloatEventChannelSO, float, object>
    {

    }
}