using System;
using System.Collections.Generic;
using UnityEngine;
using CorePackage.Common;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Manages global events and a list of local event managers. Works better by having only a single instance.
    /// </summary>
    [CreateAssetMenu(fileName = "GlobalEventManager", menuName = Project.MenuName + "/Managers/GlobalEventManager", order = 1)]
    public class GlobalEventManager : ScriptableObject
    {
        private readonly EventHub globalEventHub = new();

        private readonly List<EventHub> localEventHubList = new();

        internal void SubscribeLocalEventHub(EventHub localEventHub)
        {
            if (!localEventHubList.Contains(localEventHub))
                localEventHubList.Add(localEventHub);
        }

        internal void UnsubscribeLocalEventHub(EventHub localEventHub)
        {
            localEventHubList.Remove(localEventHub);
        }


        public void Subscribe<T>(Action<T> listener) where T : IEventArgs
        {
            globalEventHub.Subscribe(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : IEventArgs
        {
            globalEventHub.Unsubscribe(listener);
        }

        public void UnsubscribeAllOfType<T>() where T : IEventArgs
        {
            globalEventHub.UnsubscribeAllOfType<T>();
        }

        public void UnsubscribeAll()
        {
            globalEventHub.UnsubscribeAll();
        }

        public void Invoke<T>(T eventArgs) where T : IEventArgs
        {
            globalEventHub.Invoke(eventArgs);

            foreach (EventHub localEventHub in localEventHubList)
            {
                localEventHub.Invoke(eventArgs);
            }
        }

    }
}
