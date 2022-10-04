using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows two arguments: GameObject, object
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/GameObject Event", order = 2)]
    public class GameObjectEventChannelSO : BaseEventChannelSO<GameObject, object> { } 
}
