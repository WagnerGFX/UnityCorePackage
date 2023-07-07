using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with one argument: object
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Basic Object Listener", 19)]
    public class BasicObjectEventListener : BaseEventListener<BasicObjectEventChannelSO, object>
    {

    }
}
