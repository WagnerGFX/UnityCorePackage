using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with two arguments: boolean, object
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Bool Listener", 1)]
    public class BoolEventListener : BaseEventListener<BoolEventChannelSO, bool, object>
    {

    }
}