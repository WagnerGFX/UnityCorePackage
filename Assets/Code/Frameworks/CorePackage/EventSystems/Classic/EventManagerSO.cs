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
    public sealed class EventManagerSO : ScriptableObject, IEventManager
    {
        private readonly EventManager globalEventHub = new EventManager();

        private readonly List<EventManager> localEventHubList = new();

        internal void SubscribeLocalEventHub(EventManager localEventHub)
        {
            if (!localEventHubList.Contains(localEventHub))
                localEventHubList.Add(localEventHub);
        }

        internal void UnsubscribeLocalEventHub(EventManager localEventHub)
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

            foreach (EventManager localEventHub in localEventHubList)
            {
                localEventHub.Invoke(eventArgs);
            }
        }
    }
}