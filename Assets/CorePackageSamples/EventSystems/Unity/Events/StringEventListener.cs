using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with two arguments: string, object
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "String Listener", 1)]
    public class StringEventListener : BaseEventListener<StringEventChannelSO, string, object>
    {

    }
}
