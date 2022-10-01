using ComradeVanti.CSharpTools;
using Dev.ComradeVanti;
using UnityEngine;

namespace GatherCraftDefend
{

    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField] private Transform enemyParentTransform;

        private EnemyTypeRepo enemyTypeRepo;


        private void Awake() =>
            enemyTypeRepo = EnemyTypeRepo.LoadFromResources();


        public IOpt<Nothing> TrySpawnEnemyWithName(string name) =>
            enemyTypeRepo.TryGetByName(name)
                         .Map(SpawnEnemyOfType);

        private Nothing SpawnEnemyOfType(EnemyType type)
        {
            Instantiate(type.Prefab, enemyParentTransform);
            return Nothing.atAll;
        }

    }

}