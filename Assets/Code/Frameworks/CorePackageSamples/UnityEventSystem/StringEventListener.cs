using UnityEngine;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    [AddComponentMenu("MyProjectName/Event Listeners/String", 1)]
    public class StringEventListener : BaseEventListener<StringEventChannelSO, string, object>
    {

    }
}