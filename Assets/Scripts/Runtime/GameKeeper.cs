using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatherCraftDefend
{
    public class GameKeeper : MonoBehaviour
    {

        public void OnEnemyReachedStash()
        {
            GameOverBecause(GameOverReason.StashLost);
        }

        private void GameOverBecause(GameOverReason reason)
        {
            Debug.Log(reason switch
            {
                // TODO: Add actual game-over
                GameOverReason.StashLost => "ENEMY REACHED STASH"
            });
        }

    }
}
