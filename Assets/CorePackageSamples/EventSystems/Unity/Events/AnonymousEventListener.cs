using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with no arguments.
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Anonymous Listener", 21)]
    public class AnonymousEventListener : BaseEventListener<AnonymousEventChannelSO>
    {

    }
}
