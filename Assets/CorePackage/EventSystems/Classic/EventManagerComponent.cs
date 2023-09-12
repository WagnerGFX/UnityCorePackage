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
        private EventManagerSO _globalEventManager;

        [SerializeField]
        [Tooltip("When the scope is undefined, the manager will send events globally when enabled or locally when disabled.")]
        private bool _prioritizeEventsAsGlobal = true;

        private readonly IEventManager _eventManager = new EventManager();

        private void Awake()
        {
            if (!_globalEventManager)
            { _globalEventManager = Resources.FindObjectsOfTypeAll<EventManagerSO>().FirstOrDefault(); }

            if (_globalEventManager)
            { _globalEventManager.SubscribeLocalEventManager(_eventManager); }
        }

        private void OnDestroy()
        {
            if (_globalEventManager)
            { _globalEventManager.UnsubscribeLocalEventManager(_eventManager); }

            UnsubscribeAll();
        }


        public void Invoke<T>(T eventArgs) where T : IEventArgs
        {
            Invoke(eventArgs, _prioritizeEventsAsGlobal);
        }

        public void Invoke<T>(T eventArgs, bool asGlobal) where T : IEventArgs
        {
            if (asGlobal && _globalEventManager)
            { _globalEventManager.Invoke(eventArgs); }
            else
            { _eventManager.Invoke(eventArgs); }
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
            _eventManager.Subscribe(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : IEventArgs
        {
            _eventManager.Unsubscribe(listener);
        }

        public void UnsubscribeAll()
        {
            _eventManager.UnsubscribeAll();
        }

        public void UnsubscribeAllOfType<T>() where T : IEventArgs
        {
            _eventManager.UnsubscribeAllOfType<T>();
        }
    }
}
