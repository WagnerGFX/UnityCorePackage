using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CorePackage.UnityEventSystem
{
    public abstract class BaseEventChannelSO<TValue> : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] [TextArea]
        private string editorDescription;
#endif

        private event UnityAction<TValue> EventChannel;

        public event Action<ScriptableObject, TValue, Delegate[]> OnEventRaised;
        public event Action<ScriptableObject, TValue> OnEventRaisedWithNoListeners;
        public event Action<ScriptableObject, UnityAction<TValue>, bool> OnEventSubscribed;
        public event Action<ScriptableObject, UnityAction<TValue>, bool> OnEventUnsubscribed;
        public event Action<ScriptableObject, object> OnEventListenersCleared;

        public void RaiseEvent(TValue value)
        {
            if (EventChannel != null)
            {
                EventChannel.Invoke(value);
                OnEventRaised?.Invoke(this, value, EventChannel.GetInvocationList());
            }
            else
                OnEventRaisedWithNoListeners?.Invoke(this, value);
        }

        public bool Subscribe(UnityAction<TValue> action)
        {
            bool canSubscribe = !IsSubscribed(action);

            if (canSubscribe)
                EventChannel += action;

            OnEventSubscribed?.Invoke(this, action, canSubscribe);
            return canSubscribe;
        }

        public bool Unsubscribe(UnityAction<TValue> action)
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
                    EventChannel -= eventDelegate as UnityAction<TValue>;
            }

            OnEventListenersCleared?.Invoke(this, sender);
        }

        private bool IsSubscribed(UnityAction<TValue> action)
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