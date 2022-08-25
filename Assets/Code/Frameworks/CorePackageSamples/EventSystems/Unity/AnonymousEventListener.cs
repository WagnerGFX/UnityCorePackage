using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;

namespace MyProjectName.Events
{
    [AddComponentMenu(Project.MenuName + "/Event Listeners/Anonymous", 19)]
    public class AnonymousEventListener : BaseEventListener<AnonymousEventChannelSO>
    {

    }
}
