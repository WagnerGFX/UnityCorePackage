using UnityEngine;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    [AddComponentMenu("MyProjectName/Event Listeners/Int", 1)]
    public class IntEventListener : BaseEventListener<IntEventChannelSO, int, object>
    {

    }
}