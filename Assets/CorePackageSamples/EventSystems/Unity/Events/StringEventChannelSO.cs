using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows two arguments: string, object
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/String Event", order = 1)]
    public class StringEventChannelSO : BaseEventChannelSO<string, object> { } 
}
