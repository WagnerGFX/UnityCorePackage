using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows one argument: GameObject
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Basic GameObject Event", order = 19)]
    public class BasicGameObjectEventChannelSO : BaseEventChannelSO<GameObject> { } 
}
