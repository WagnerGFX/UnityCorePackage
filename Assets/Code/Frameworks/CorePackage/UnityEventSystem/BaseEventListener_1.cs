using UnityEngine;
using UnityEngine.Events;

namespace CorePackage.UnityEventSystem
{
    /// <summary>
    /// A flexible handler for events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
    /// </summary>
    public abstract class BaseEventListener<TChannel, TValue> : MonoBehaviour where TChannel : BaseEventChannelSO<TValue>
    {
        [SerializeField]
        private TChannel _channel;

        [SerializeField]
        private UnityAction<TValue> OnEventRaised;

        private void OnEnable()
        {
            if (_channel != null)
                _channel?.Subscribe(Respond);
        }

        private void OnDisable()
        {
            if (_channel != null)
                _channel?.Unsubscribe(Respond);
        }

        private void Respond(TValue value)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value);
        }
    }
}
