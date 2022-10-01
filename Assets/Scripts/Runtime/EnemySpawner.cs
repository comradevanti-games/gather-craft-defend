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

        public IOpt<GameObject> TrySpawnEnemyWithName(string name) =>
            enemyTypes.TryGetById(name)
                      .Map(SpawnEnemyOfType);

        private GameObject SpawnEnemyOfType(EnemyType type) =>
            Instantiate(type.Prefab, enemyParentTransform);

        private static ResourceRepo<string, EnemyType> LoadEnemyTypes() =>
            ResourceRepo<string, EnemyType>.Load(ResourcePath, it => it.name);

    }

}