using ComradeVanti.CSharpTools;
using UnityEngine;

namespace GatherCraftDefend
{

    public class EnemySpawner : MonoBehaviour
    {

        private const string ResourcePath = "EnemyTypes";

        [SerializeField] private float minSpawnDistance;
        [SerializeField] private float maxSpawnDistance;
        [SerializeField] private Transform enemyParentTransform;

        private ResourceRepo<string, EnemyType> enemyTypes;


        private void Awake() =>
            enemyTypes = LoadEnemyTypes();

        public IOpt<Enemy> TrySpawnEnemyWithTypeName(string name) =>
            enemyTypes.TryGetById(name)
                      .Map(SpawnEnemyOfType);

        private Enemy SpawnEnemyOfType(EnemyType type)
        {
            var position = RandomSpawnPosition();
            var enemyGameObject = Object.Instantiate(
                type.Prefab, position, Quaternion.identity, enemyParentTransform);
            return new Enemy(enemyGameObject, 1);
        }

        private Vector2 RandomSpawnPosition() =>
            SpawnRing.GeneratePoint(minSpawnDistance, maxSpawnDistance);

        private static ResourceRepo<string, EnemyType> LoadEnemyTypes() =>
            ResourceRepo<string, EnemyType>.Load(ResourcePath, it => it.name);

    }

}