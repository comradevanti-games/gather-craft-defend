using System;
using Dev.ComradeVanti;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GatherCraftDefend
{

    public class Enemy
    {

        private readonly GameObject gameObject;


        public Enemy(GameObject gameObject) =>
            this.gameObject = gameObject;

        
        public event Action<Nothing> OnDied;


        public void Kill()
        {
            Object.Destroy(gameObject);
            OnDied?.Invoke(Nothing.atAll);
        }

    }

}