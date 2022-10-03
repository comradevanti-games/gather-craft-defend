using System;
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
            Mathf.FloorToInt(60f / (1 + Mathf.Pow((float)Math.E, -0.2f * night)) - 30f);

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