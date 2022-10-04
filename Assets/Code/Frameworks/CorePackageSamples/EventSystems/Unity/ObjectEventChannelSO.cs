using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows two arguments: object, object
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Object Event", order = 2)]
    public class ObjectEventChannelSO : BaseEventChannelSO<object, object> { } 
}
