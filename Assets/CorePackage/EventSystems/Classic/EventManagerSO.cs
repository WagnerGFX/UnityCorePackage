using System;
using System.Collections.Generic;
using UnityEngine;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// ScriptableObject-based event manager. Manages global events and a list of local event managers. Works better by having only a single instance.
    /// </summary>
    [CreateAssetMenu(fileName = "GlobalEventManager", menuName = "CorePackage/Global Event Manager", order = 1)]
    public sealed class EventManagerSO : ScriptableObject, IEventManagerGlobal
    {
        private readonly IEventManager _eventManager = new EventManager();

        private readonly List<IEventManager> _localEventManagerList = new();


        public void Invoke<T>(T eventArgs) where T : IEventArgs
        {
            _eventManager.Invoke(eventArgs);

            foreach (IEventManager eventManager in _localEventManagerList)
            {
                if (eventManager is IEventManagerLocal localEventManager)
                {
                    localEventManager.InvokeLocal(eventArgs);
                }
                else
                {
                    eventManager.Invoke(eventArgs);
                }
            }
        }

        public void Subscribe<T>(Action<T> listener) where T : IEventArgs
        {
            _eventManager.Subscribe(listener);
        }

        public void SubscribeLocalEventManager(IEventManager localEventManager)
        {
            if (!_localEventManagerList.Contains(localEventManager))
            { _localEventManagerList.Add(localEventManager); }
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

        public void UnsubscribeLocalEventManager(IEventManager localEventManager)
        {
            _localEventManagerList.Remove(localEventManager);
        }
    }
}
