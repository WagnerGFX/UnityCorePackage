using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows one argument: boolean
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Basic Bool Event", order = 18)]
    public class BasicBoolEventChannelSO : BaseEventChannelSO<bool> { } 
}
