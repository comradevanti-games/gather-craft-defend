using UnityEngine;

namespace GatherCraftDefend
{

    public class GameKeeper : MonoBehaviour
    {

        [SerializeField] private LoadScene loadScene;

        public void OnEnemyReachedStash() =>
            GameOverBecause(GameOverReason.StashLost);

        private void GameOverBecause(GameOverReason reason)
        {
            var parameters =
                SceneLoadParams.empty.AddInt("reason", (int)reason);
            loadScene.WithName("GameOver", parameters);
        }

    }

}