using UnityEngine;
using UnityEngine.Events;

namespace CorePackage.EventSystems.Unity
{
    /// <summary>
    /// A flexible handler for events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
    /// </summary>
    public abstract class BaseEventListener<TChannel, TValue> : MonoBehaviour where TChannel : BaseEventChannelSO<TValue>
    {
        [SerializeField]
        private TChannel _channel;

        [SerializeField]
        private bool _disableWithObject;

        [SerializeField]
        private UnityEvent<TValue> OnEventRaised;

        private void OnEnable()
        {
            _channel?.Subscribe(Respond);
        }

        private void OnDisable()
        {
            if (_disableWithObject)
                _channel?.Unsubscribe(Respond);
        }

        private void OnDestroy()
        {
            if (!_disableWithObject)
                _channel?.Unsubscribe(Respond);
        }

        private void Respond(TValue value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}
