using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with two arguments: string, object
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/String Listener", 1)]
    public class StringEventListener : BaseEventListener<StringEventChannelSO, string, object>
    {

    }
}