using ComradeVanti.CSharpTools;
using Dev.ComradeVanti;
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


        public IOpt<Nothing> TrySpawnEnemyWithName(string name) =>
            enemyTypes.TryGetById(name)
                      .Map(SpawnEnemyOfType);

        private Nothing SpawnEnemyOfType(EnemyType type)
        {
            Instantiate(type.Prefab, enemyParentTransform);
            return Nothing.atAll;
        }

        private static ResourceRepo<string, EnemyType> LoadEnemyTypes() =>
            ResourceRepo<string, EnemyType>.Load(ResourcePath, it => it.name);

    }

}