using UnityEngine;
using UnityEngine.Events;

namespace GatherCraftDefend
{

    public class HealthKeeper : MonoBehaviour
    {

        [SerializeField] private int baseHealth;
        [SerializeField] private bool destroyOnZeroHealth;
        [SerializeField] private UnityEvent<int> onHealthChanged;

        private int health;


        public int Health
        {
            get => health;
            set
            {
                var clamped = Mathf.Max(value, 0);
                if (health != clamped)
                {
                    health = clamped;
                    onHealthChanged.Invoke(health);
                }

                if (health == 0 && destroyOnZeroHealth)
                    Destroy(gameObject);
            }
        }

        public UnityEvent<int> OnHealthChanged => onHealthChanged;


        private void Awake() =>
            Health = baseHealth;

        public void Damage(int damage = 1) =>
            Health -= damage;

    }

}