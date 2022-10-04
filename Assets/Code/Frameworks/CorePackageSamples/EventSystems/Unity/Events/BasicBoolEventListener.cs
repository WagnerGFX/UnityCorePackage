using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with one argument: boolean
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Basic Bool Listener", 18)]
    public class BasicBoolEventListener : BaseEventListener<BasicBoolEventChannelSO, bool>
    {

    }
}