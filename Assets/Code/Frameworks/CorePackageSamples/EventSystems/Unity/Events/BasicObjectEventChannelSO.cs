using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows one argument: object. Useful to send the event caller as argument.
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Basic Object Event", order = 19)]
    public class BasicObjectEventChannelSO : BaseEventChannelSO<object> { } 
}