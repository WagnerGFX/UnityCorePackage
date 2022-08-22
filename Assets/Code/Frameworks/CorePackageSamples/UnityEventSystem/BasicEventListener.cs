using UnityEngine;
using CorePackage.Common;
using CorePackage.UnityEventSystem;

namespace MyProjectName.Events
{
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Basic", 18)]
    public class BasicEventListener : BaseEventListener<BasicEventChannelSO, object>
    {

    }
}
