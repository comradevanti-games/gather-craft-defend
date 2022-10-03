using System;
using UnityEngine;

namespace GatherCraftDefend
{

    public class GameKeeper : MonoBehaviour
    {

        [SerializeField] private LoadScene loadScene;

        private void Awake() {
            GL.ClearWithSkybox(true,Camera.main);
        }

        public void OnEnemyReachedStash() =>
            GameOverBecause(GameOverReason.StashLost);

        public void OnPlayerHealthChanged(int health)
        {
            if (health == 0)
                OnPlayerDied();
        }

        private void OnPlayerDied() =>
            GameOverBecause(GameOverReason.Killed);

        private void GameOverBecause(GameOverReason reason)
        {
            var parameters =
                SceneLoadParams.empty.AddInt("reason", (int)reason);
            loadScene.WithName("GameOver", parameters);
        }

    }

}