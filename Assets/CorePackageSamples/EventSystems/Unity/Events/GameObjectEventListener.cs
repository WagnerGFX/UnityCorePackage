using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with two arguments: GameObject, object
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/GameObject Listener", 2)]
    public class GameObjectEventListener : BaseEventListener<GameObjectEventChannelSO, GameObject, object>
    {

    }
}