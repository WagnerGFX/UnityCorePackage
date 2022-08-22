using UnityEngine;
using CorePackage.Common;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    /// <summary>
    /// This class is used for Events that have one GameObject argument.
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/GameObject Event", order = 2)]
    public class GameObjectEventChannelSO : BaseEventChannelSO<GameObject, object> { } 
}
