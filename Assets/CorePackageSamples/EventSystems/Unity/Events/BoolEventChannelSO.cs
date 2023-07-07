using UnityEngine;
using CorePackage.EventSystems.Unity;
using System.Runtime.CompilerServices;

namespace MyProjectName.Events
{
    /// <summary>
    /// Event SO that allows two arguments: boolean, object
    /// </summary>
    [CreateAssetMenu(menuName = EventsConsts.MENU_EVENT_CHANNELS + "Bool Event", order = 1)]
    public class BoolEventChannelSO : BaseEventChannelSO<bool, object>
    {
        public void RaiseEvent(bool value, [CallerMemberName] string ttt ="")
        {
            Debug.Log(ttt);
            RaiseEvent(value, ttt);
        }
    } 
}
