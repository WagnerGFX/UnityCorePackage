using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows two arguments: float, object
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Float Event", order = 1)]
    public class FloatEventChannelSO : BaseEventChannelSO<float, object> { } 
}
