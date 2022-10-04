using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with one argument: float
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Basic Float Listener", 18)]
    public class BasicFloatEventListener : BaseEventListener<BasicFloatEventChannelSO, float>
    {

    }
}