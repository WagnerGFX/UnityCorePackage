using UnityEngine;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    [AddComponentMenu("MyProjectName/Event Listeners/GameObject", 2)]
    public class GameObjectEventListener : BaseEventListener<GameObjectEventChannelSO, GameObject, object>
    {

    }
}