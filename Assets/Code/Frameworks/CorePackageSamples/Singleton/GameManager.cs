using UnityEngine;
using CorePackage.Common;
using CorePackage.Singleton;

namespace MyProjectName.Management
{
    [CreateAssetMenu(fileName = "GameManager", menuName = Project.MenuName + "/Managers/GameManager", order = 1)]
    public sealed class GameManager : ScriptableSingletonObject<GameManager>
    {
        public GameState CurrentGameState { get; private set; } = GameState.None;

        public void ChangeGameState(GameState newState)
        {
            GameState previousGameState = CurrentGameState;
            CurrentGameState = newState;

            //OnGameStateChanged
        }
    }
}