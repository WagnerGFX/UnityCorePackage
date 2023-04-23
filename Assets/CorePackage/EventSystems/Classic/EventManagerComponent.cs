using System;
using System.Linq;
using UnityEngine;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Componenet-based event manager. Can fire and receive events locally or globally via a ScriptableObject.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class EventManagerComponent : MonoBehaviour, IEventManagerLocal
    {
        [SerializeField]
        private EventManagerSO globalEventManager;

        [SerializeField]
        [Tooltip("When the scope is undefined, the manager will send events globally when enabled or locally when disabled.")]
        private bool prioritizeEventsAsGlobal = true;

        private readonly IEventManager eventManager = new EventManager();

        private void Awake()
        {
            if (!globalEventManager)
                globalEventManager = Resources.FindObjectsOfTypeAll<EventManagerSO>().FirstOrDefault();

            globalEventManager?.SubscribeLocalEventManager(eventManager);
        }

        private void OnDestroy()
        {
            globalEventManager?.UnsubscribeLocalEventManager(eventManager);
            UnsubscribeAll();
        }


        public void Invoke<T>(T eventArgs) where T : IEventArgs
        {
            Invoke(eventArgs, prioritizeEventsAsGlobal);
        }

        public void Invoke<T>(T eventArgs, bool asGlobal) where T : IEventArgs
        {
            if (asGlobal && globalEventManager)
                globalEventManager.Invoke(eventArgs);
            else
                eventManager.Invoke(eventArgs);
        }

        public void InvokeGlobal<T>(T eventArgs) where T : IEventArgs
        {
            Invoke(eventArgs, true);
        }

        public void InvokeLocal<T>(T eventArgs) where T : IEventArgs
        {
            Invoke(eventArgs, false);
        }

        public void Subscribe<T>(Action<T> listener) where T : IEventArgs
        {
            eventManager.Subscribe(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : IEventArgs
        {
            eventManager.Unsubscribe(listener);
        }

        public void UnsubscribeAll()
        {
            eventManager.UnsubscribeAll();
        }

        public void UnsubscribeAllOfType<T>() where T : IEventArgs
        {
            eventManager.UnsubscribeAllOfType<T>();
        }
    }
}