using UnityEngine;
using CorePackage.Common;
using CorePackage.EventSystems.Unity;
using System.Runtime.CompilerServices;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows two arguments: boolean, object
    /// </summary>
    [CreateAssetMenu(menuName = Project.MenuName + "/Event Channels/Bool Event", order = 1)]
    public class BoolEventChannelSO : BaseEventChannelSO<bool, object>
    {
        public void RaiseEvent(bool value, [CallerMemberName] string ttt ="")
        {
            Debug.Log(ttt);
            RaiseEvent(value, ttt);
        }
    } 
}
