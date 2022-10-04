using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with one argument: GameObject
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Basic GameObject Listener", 19)]
    public class BasicGameObjectEventListener : BaseEventListener<BasicGameObjectEventChannelSO, GameObject>
    {

    }
}