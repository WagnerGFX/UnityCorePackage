using CorePackage.Singleton;
using UnityEngine;

namespace CorePackageSamples.Singleton
{
    [CreateAssetMenu(fileName = "GameManager", menuName = "CorePackageSamples/GameManager", order = 1)]
    public sealed class GameManager : ScriptableSingletonObject<GameManager>
    {
        public GameState CurrentGameState { get; private set; } = GameState.None;

        public void ChangeGameState(GameState newState)
        {
            GameState previousGameState = CurrentGameState;
            CurrentGameState = newState;

            // Fire OnGameStateChanged Event
        }
    }
}
