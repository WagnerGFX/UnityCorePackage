using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// This class is used for Events that have one int argument.
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Int Event", order = 1)]
    public class IntEventChannelSO : BaseEventChannelSO<int, object> { } 
}
