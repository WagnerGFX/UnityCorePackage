using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with two arguments: object, object
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Object Listener", 2)]
    public class ObjectEventListener : BaseEventListener<ObjectEventChannelSO, object, object>
    {

    }
}