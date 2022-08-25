using UnityEngine;
using UnityEngine.Events;

namespace CorePackage.EventSystems.Unity
{
    /// <summary>
    /// A flexible handler for events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
    /// </summary>
    public abstract class BaseEventListener<TChannel> : MonoBehaviour where TChannel : BaseEventChannelSO
    {
        [SerializeField]
        private TChannel _channel;

        [SerializeField]
        private UnityEvent OnEventRaised;

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

        private void Respond()
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke();
        }
    }
}