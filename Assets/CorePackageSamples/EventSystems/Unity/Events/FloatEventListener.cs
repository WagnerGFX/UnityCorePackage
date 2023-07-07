using UnityEngine;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with two arguments: float, object
    /// </summary>
    [AddComponentMenu(EventsConsts.MENU_EVENT_LISTENERS + "Float Listener", 1)]
    public class FloatEventListener : BaseEventListener<FloatEventChannelSO, float, object>
    {

    }
}
