using UnityEngine;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    [AddComponentMenu("MyProjectName/Event Listeners/Float", 1)]
    public class FloatEventListener : BaseEventListener<FloatEventChannelSO, float, object>
    {

    }
}