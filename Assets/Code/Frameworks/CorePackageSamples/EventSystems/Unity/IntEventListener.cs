using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with two arguments: int, object
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Int Listener", 1)]
    public class IntEventListener : BaseEventListener<IntEventChannelSO, int, object>
    {

    }
}