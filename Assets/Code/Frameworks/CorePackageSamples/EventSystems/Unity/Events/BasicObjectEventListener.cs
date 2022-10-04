using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with one argument: object
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Basic Object Listener", 19)]
    public class BasicObjectEventListener : BaseEventListener<BasicObjectEventChannelSO, object>
    {

    }
}
