using System.Collections.Generic;
using UnityEngine;

namespace GatherCraftDefend
{

    public class EnemyKeeper : MonoBehaviour
    {

        [SerializeField] private EnemySpawner enemySpawner;

        private int night;
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

        private void StartWave()
        {
            night++;
            var enemyCount = CalculateEnemyCountForNight();
            for (var i = 0; i < enemyCount; i++) SpawnEnemy();
        }

        private int CalculateEnemyCountForNight() =>
            Mathf.Max(Mathf.FloorToInt(night * 0.6f), 1);

        private void KillAllEnemies() =>
            aliveEnemies.ToArray().Iter(it => it.Kill());

        private void SpawnEnemy()
        {
            var enemy = enemySpawner.SpawnEnemy();
            aliveEnemies.Add(enemy);
            enemy.OnDied += _ => OnEnemyDied(enemy);
        }

        private void OnEnemyDied(Enemy enemy) =>
            aliveEnemies.Remove(enemy);

    }

}