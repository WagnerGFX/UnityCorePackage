using System;
using System.Linq;
using UnityEngine;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Instance-based event manager. Can fire and receive events locally or globally.
    /// </summary>
    public sealed class EventManagerComponent : MonoBehaviour, IEventManagerLocal
    {
        [SerializeField]
        private EventManagerSO globalEventManager = default;

        private readonly IEventManager localEventHub = new EventManager();

        private void Awake()
        {
            if (!globalEventManager)
                globalEventManager = Resources.FindObjectsOfTypeAll<EventManagerSO>().FirstOrDefault();

            globalEventManager?.SubscribeLocalEventHub(localEventHub);
        }

        private void OnDestroy()
        {
            globalEventManager?.UnsubscribeLocalEventHub(localEventHub);
            UnsubscribeAll();
        }


        public void Subscribe<T>(Action<T> listener) where T : IEventArgs
        {
            localEventHub.Subscribe(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : IEventArgs
        {
            localEventHub.Unsubscribe(listener);
        }

        public void UnsubscribeAllOfType<T>() where T : IEventArgs
        {
            localEventHub.UnsubscribeAllOfType<T>();
        }

        public void UnsubscribeAll()
        {
            localEventHub.UnsubscribeAll();
        }

        public void Invoke<T>(T eventArgs, bool sendGoballly = false) where T : IEventArgs
        {
            if (sendGoballly)
                globalEventManager?.Invoke(eventArgs);
            else
                localEventHub.Invoke(eventArgs);
        }

        public void Invoke<T>(T eventArgs) where T : IEventArgs
        {
            if (globalEventManager)
                globalEventManager?.Invoke(eventArgs);
            else
                localEventHub.Invoke(eventArgs);
        }

        public void InvokeGlobal<T>(T eventArgs) where T : IEventArgs
        {
            globalEventManager?.Invoke(eventArgs);
        }

        public void InvokeLocal<T>(T eventArgs) where T : IEventArgs
        {
            localEventHub.Invoke(eventArgs);
        }
    }
}