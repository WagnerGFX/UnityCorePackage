using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CorePackage.UnityEventSystem
{
    /// <summary>
    /// Anonymous Event Channel, preffer to use EventChannelBaseSO<object> with the sender data.
    /// </summary>
    public abstract class BaseEventChannelSO : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] [TextArea]
        private string editorDescription;
#endif

        private event UnityAction EventChannel;

        public event Action<ScriptableObject, Delegate[]> OnEventRaised;
        public event Action<ScriptableObject> OnEventRaisedWithNoListeners;
        public event Action<ScriptableObject, UnityAction, bool> OnEventSubscribed;
        public event Action<ScriptableObject, UnityAction, bool> OnEventUnsubscribed;
        public event Action<ScriptableObject, object> OnEventListenersCleared;

        public void RaiseEvent()
        {
            if (EventChannel != null)
            {
                EventChannel.Invoke();
                OnEventRaised?.Invoke(this, EventChannel.GetInvocationList());
            }
            else
                OnEventRaisedWithNoListeners?.Invoke(this);
        }

        public bool Subscribe(UnityAction action)
        {
            bool canSubscribe = !IsSubscribed(action);

            if (canSubscribe)
                EventChannel += action;

            OnEventSubscribed?.Invoke(this, action, canSubscribe);
            return canSubscribe;
        }

        public bool Unsubscribe(UnityAction action)
        {
            bool wasSubscribed = IsSubscribed(action);

            if (wasSubscribed)
                EventChannel -= action;

            OnEventUnsubscribed?.Invoke(this, action, wasSubscribed);
            return wasSubscribed;
        }

        public void UnsubscribeAll(object sender)
        {
            if (EventChannel != null && EventChannel.GetInvocationList() != null)
            {
                foreach (Delegate eventDelegate in EventChannel.GetInvocationList())
                    EventChannel -= eventDelegate as UnityAction;
            }

            OnEventListenersCleared?.Invoke(this, sender);
        }

        private bool IsSubscribed(UnityAction action)
        {
            bool isSubscribed = false;

            if (EventChannel != null && EventChannel.GetInvocationList() != null)
                if (EventChannel.GetInvocationList().Contains(action))
                    isSubscribed = true;

            return isSubscribed;
        }

        // Global Debug
        //private void OnEnable()
        //    => this.OnEventRaised += EventDebugUtilities.DebugRaisedEvent;
        //private void OnDisable()
        //    => this.OnEventRaised -= EventDebugUtilities.DebugRaisedEvent;
    }
}