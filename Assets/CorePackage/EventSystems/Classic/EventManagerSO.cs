using CorePackage.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// ScriptableObject-based event manager. Manages global events and a list of local event managers. Works better by having only a single instance.
    /// </summary>
    [CreateAssetMenu(fileName = "GlobalEventManager", menuName = Project.MenuName + "/Managers/GlobalEventManager", order = 1)]
    public sealed class EventManagerSO : ScriptableObject, IEventManagerGlobal
    {
        private readonly IEventManager eventManager = new EventManager();

        private readonly List<IEventManager> localEventManagerList = new();


        public void Invoke<T>(T eventArgs) where T : IEventArgs
        {
            eventManager.Invoke(eventArgs);

            foreach (IEventManager eventManager in localEventManagerList)
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
            eventManager.Subscribe(listener);
        }

        public void SubscribeLocalEventManager(IEventManager localEventManager)
        {
            if (!localEventManagerList.Contains(localEventManager))
                localEventManagerList.Add(localEventManager);
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

        public void UnsubscribeLocalEventManager(IEventManager localEventManager)
        {
            localEventManagerList.Remove(localEventManager);
        }

    }
}