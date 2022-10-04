using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with no arguments.
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Anonymous Listener", 21)]
    public class AnonymousEventListener : BaseEventListener<AnonymousEventChannelSO>
    {

    }
}
