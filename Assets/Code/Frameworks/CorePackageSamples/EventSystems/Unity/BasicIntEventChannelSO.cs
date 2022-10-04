using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows one argument: int
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Basic Int Event", order = 18)]
    public class BasicIntEventChannelSO : BaseEventChannelSO<int> { } 
}
