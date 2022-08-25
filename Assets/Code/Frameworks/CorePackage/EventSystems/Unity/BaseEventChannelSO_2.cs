using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CorePackage.EventSystems.Unity
{
    public abstract class BaseEventChannelSO<TValue,TSender> : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] [TextArea]
        private string editorDescription;
#endif

        private event UnityAction<TValue, TSender> EventChannel;

        public event Action<ScriptableObject, TValue, TSender, Delegate[]> OnEventRaised;
        public event Action<ScriptableObject, TValue, TSender> OnEventRaisedWithNoListeners;
        public event Action<ScriptableObject, UnityAction<TValue, TSender>, bool> OnEventSubscribed;
        public event Action<ScriptableObject, UnityAction<TValue, TSender>, bool> OnEventUnsubscribed;
        public event Action<ScriptableObject, object> OnEventListenersCleared;

        public void RaiseEvent(TValue value, TSender sender)
        {
            if (EventChannel != null)
            {
                EventChannel.Invoke(value, sender);
                OnEventRaised?.Invoke(this, value, sender, EventChannel.GetInvocationList());
            }
            else
                OnEventRaisedWithNoListeners?.Invoke(this, value, sender);
        }

        public bool Subscribe(UnityAction<TValue, TSender> action)
        {
            bool canSubscribe = !IsSubscribed(action);

            if (canSubscribe)
                EventChannel += action;

            OnEventSubscribed?.Invoke(this, action, canSubscribe);
            return canSubscribe;
        }

        public bool Unsubscribe(UnityAction<TValue, TSender> action)
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
                    EventChannel -= eventDelegate as UnityAction<TValue, TSender>;
            }

            OnEventListenersCleared?.Invoke(this, sender);
        }

        private bool IsSubscribed(UnityAction<TValue, TSender> action)
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