using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows one argument: string
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Basic String Event", order = 18)]
    public class BasicStringEventChannelSO : BaseEventChannelSO<string> { } 
}
