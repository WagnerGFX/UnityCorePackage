using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with two arguments: float, object
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Float Listener", 1)]
    public class FloatEventListener : BaseEventListener<FloatEventChannelSO, float, object>
    {

    }
}