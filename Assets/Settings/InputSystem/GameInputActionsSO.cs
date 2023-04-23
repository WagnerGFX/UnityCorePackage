using UnityEngine;
using CorePackage.Common;

[CreateAssetMenu(fileName = "newGameInput", menuName = Project.MenuName + "/Game Input SO")]
public class GameInputActionsSO : ScriptableObject
{
    public GameInputActions Input { get; private set; }
    public GameInputActions.PlayerActions PlayerActions => Input.Player;
    public GameInputActions.UIActions UIActions => Input.UI;

    void OnEnable()
    {
        Input = new GameInputActions();
    }

    public void EnablePlayerActions()
    {
        Input.Player.Enable();
        Input.UI.Disable();
    }
    public void EnableUIActions()
    {
        Input.Player.Disable();
        Input.UI.Enable();

    }
    public void DisableAllActions()
    {
        Input.Player.Disable();
        Input.UI.Disable();
    }
}