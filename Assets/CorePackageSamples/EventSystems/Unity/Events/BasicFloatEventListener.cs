using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with one argument: float
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Basic Float Listener", 18)]
    public class BasicFloatEventListener : BaseEventListener<BasicFloatEventChannelSO, float>
    {

    }
}
