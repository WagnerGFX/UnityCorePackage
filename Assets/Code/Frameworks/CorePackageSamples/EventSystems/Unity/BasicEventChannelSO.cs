using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// This class is used for Events that the argument type is flexible or neglectable. The recommended use is to send the event caller as argument.
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Basic Event", order = 18)]
    public class BasicEventChannelSO : BaseEventChannelSO<object> { } 
}