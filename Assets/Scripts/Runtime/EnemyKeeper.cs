using UnityEngine;

namespace GatherCraftDefend
{

    public class EnemyKeeper : MonoBehaviour
    {

        [SerializeField] private EnemySpawner enemySpawner;


        public void OnPhaseChanged(DayPhase phase)
        {
            if (phase == DayPhase.Defend)
                OnBecameNight();
        }

        private void OnBecameNight() =>
            StartWave();

        private void StartWave() =>
            SpawnEnemy();

        private void SpawnEnemy() =>
            enemySpawner.TrySpawnEnemyWithName("Basic");

    }

}