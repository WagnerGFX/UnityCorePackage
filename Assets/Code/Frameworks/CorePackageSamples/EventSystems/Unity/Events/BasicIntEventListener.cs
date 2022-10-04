using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with one argument: int
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Basic Int Listener", 18)]
    public class BasicIntEventListener : BaseEventListener<BasicIntEventChannelSO, int>
    {

    }
}