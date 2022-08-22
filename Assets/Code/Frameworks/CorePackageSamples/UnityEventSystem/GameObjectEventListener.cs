using UnityEngine;
using CorePackage.Common;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    [AddComponentMenu(Project.MenuName + "/Event Listeners/GameObject", 2)]
    public class GameObjectEventListener : BaseEventListener<GameObjectEventChannelSO, GameObject, object>
    {

    }
}