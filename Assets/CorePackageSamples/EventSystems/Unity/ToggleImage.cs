using UnityEngine;

namespace CorePackageSamples.UnityEvents
{
    public class ToggleImage : MonoBehaviour
    {
        [SerializeField]
        private BasicGameObjectEventChannelSO _onToggle;

        [SerializeField]
        private bool _startDisabled = false;

        private void Start()
        {
            if (_startDisabled)
            { gameObject.SetActive(false); }
        }

        public void ToggleObject(GameObject sender)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void ToggleConnection(bool connect)
        {
            if (connect)
            { _onToggle.Subscribe(ToggleObject); }
            else
            { _onToggle.Unsubscribe(ToggleObject); }
        }
    }
}
