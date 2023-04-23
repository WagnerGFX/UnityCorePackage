using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    /// <summary>
    /// Listener for events with one argument: string
    /// </summary>
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Basic String Listener", 18)]
    public class BasicStringEventListener : BaseEventListener<BasicStringEventChannelSO, string>
    {

    }
}