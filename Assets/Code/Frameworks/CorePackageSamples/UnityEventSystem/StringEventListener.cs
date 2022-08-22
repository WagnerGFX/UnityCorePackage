﻿using UnityEngine;
using CorePackage.Common;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    [AddComponentMenu(Project.MenuName + "/Event Listeners/String", 1)]
    public class StringEventListener : BaseEventListener<StringEventChannelSO, string, object>
    {

    }
}