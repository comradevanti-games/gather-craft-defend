using System;
using Dev.ComradeVanti;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GatherCraftDefend
{

    public class Enemy
    {

        private readonly GameObject gameObject;


        public Enemy(GameObject gameObject, int health)
        {
            this.gameObject = gameObject;

            var healthKeeper = this.gameObject.GetComponent<HealthKeeper>();
            healthKeeper.Health = health;
            healthKeeper.OnHealthChanged.AddListener(OnHealthChanged);
        }


        public event Action<Nothing> OnDied;


        private void OnHealthChanged(int health)
        {
            if (health == 0)
                Kill();
        }

        public void Kill()
        {
            Object.Destroy(gameObject);
            OnDied?.Invoke(Nothing.atAll);
        }

    }

}