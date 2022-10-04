using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO with no arguments.
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Anonymous Event", order = 21)]
    public class AnonymousEventChannelSO : BaseEventChannelSO { } 
}