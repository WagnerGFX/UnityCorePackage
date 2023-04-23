using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows one argument: float
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Basic Float Event", order = 18)]
    public class BasicFloatEventChannelSO : BaseEventChannelSO<float> { } 
}
