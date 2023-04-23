using MyProjectName.Events;
using UnityEngine;

public class ToggleImage : MonoBehaviour
{
    [SerializeField]
    BasicGameObjectEventChannelSO OnToggle;

    [SerializeField]
    private bool startDisabled = false;
    
    private void Start()
    {
        if (startDisabled)
            gameObject.SetActive(false);
    }

    public void ToggleObject(GameObject sender)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ToggleConnection(bool connect)
    {
        if (connect)
        {
            OnToggle.Subscribe(ToggleObject);
        }
        else
        {
            OnToggle.Unsubscribe(ToggleObject);
        }
    }
}
