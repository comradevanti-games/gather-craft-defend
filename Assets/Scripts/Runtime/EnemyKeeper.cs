using System.Collections.Generic;
using ComradeVanti.CSharpTools;
using UnityEngine;

namespace GatherCraftDefend
{

    public class EnemyKeeper : MonoBehaviour
    {

        [SerializeField] private EnemySpawner enemySpawner;

        private readonly List<Enemy> aliveEnemies = new List<Enemy>();


        public void OnPhaseChanged(DayPhase phase)
        {
            if (phase == DayPhase.Defend)
                OnBecameNight();
            else if (phase == DayPhase.Gather)
                OnBecameDay();
        }

        private void OnBecameNight() =>
            StartWave();

        private void OnBecameDay() =>
            KillAllEnemies();

        private void StartWave() =>
            TrySpawnEnemyOfType("Basic");

        private void KillAllEnemies() =>
            aliveEnemies.ToArray().Iter(it => it.Kill());

        private void TrySpawnEnemyOfType(string typeName) =>
            enemySpawner.TrySpawnEnemyWithTypeName(typeName)
                        .Match(OnEnemySpawned,
                               () => Debug.Log($"Unknown enemy-type {typeName}."));

        private void OnEnemySpawned(Enemy enemy)
        {
            aliveEnemies.Add(enemy);
            enemy.OnDied += _ => OnEnemyDied(enemy);
        }

        private void OnEnemyDied(Enemy enemy) =>
            aliveEnemies.Remove(enemy);

    }

}