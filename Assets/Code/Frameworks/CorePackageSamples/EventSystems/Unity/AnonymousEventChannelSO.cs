using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// This class is used for Events with no arguments. Preffer to use the Basic Event whenever possible.
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Anonymous Event", order = 19)]
    public class AnonymousEventChannelSO : BaseEventChannelSO { } 
}