using UnityEngine;
using CorePackage.Common;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    /// <summary>
    /// This class is used for Events that have one float argument.
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Float Event", order = 1)]
    public class FloatEventChannelSO : BaseEventChannelSO<float, object> { } 
}
