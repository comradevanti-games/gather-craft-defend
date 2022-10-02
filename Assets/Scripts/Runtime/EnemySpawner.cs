using UnityEngine;

namespace GatherCraftDefend
{

    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField] private float minSpawnDistance;
        [SerializeField] private float maxSpawnDistance;
        [SerializeField] private Transform enemyParentTransform;
        [SerializeField] private GameObject enemyPrefab;

        public Enemy SpawnEnemy()
        {
            var position = RandomSpawnPosition();
            var enemyGameObject = Instantiate(
                enemyPrefab, position, Quaternion.identity, enemyParentTransform);
            return new Enemy(enemyGameObject, 1);
        }

        private Vector2 RandomSpawnPosition() =>
            SpawnRing.GeneratePoint(minSpawnDistance, maxSpawnDistance);

    }

}