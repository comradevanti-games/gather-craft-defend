using ComradeVanti.CSharpTools;
using UnityEngine;

namespace GatherCraftDefend
{

    public class EnemySpawner : MonoBehaviour
    {

        private const string ResourcePath = "EnemyTypes";

        [SerializeField] private Transform enemyParentTransform;

        private ResourceRepo<string, EnemyType> enemyTypes;


        private void Awake() =>
            enemyTypes = EnemySpawner.LoadEnemyTypes();

        public IOpt<Enemy> TrySpawnEnemyWithTypeName(string name) =>
            enemyTypes.TryGetById(name)
                      .Map(SpawnEnemyOfType);

        private Enemy SpawnEnemyOfType(EnemyType type) =>
            new Enemy(Instantiate(type.Prefab, enemyParentTransform));

        private static ResourceRepo<string, EnemyType> LoadEnemyTypes() =>
            ResourceRepo<string, EnemyType>.Load(ResourcePath, it => it.name);

    }

}