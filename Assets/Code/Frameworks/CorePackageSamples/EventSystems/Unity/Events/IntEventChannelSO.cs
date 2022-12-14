using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows two arguments: int, object
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Int Event", order = 1)]
    public class IntEventChannelSO : BaseEventChannelSO<int, object> { } 
}
