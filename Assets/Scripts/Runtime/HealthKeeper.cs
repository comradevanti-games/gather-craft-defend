using UnityEngine;
using UnityEngine.Events;

namespace GatherCraftDefend
{

    public class HealthKeeper : MonoBehaviour
    {

        [SerializeField] private UnityEvent<int> onHealthChanged;
        
        private int health;


        public int Health
        {
            get => health;
            set
            {
                if (health != value)
                {
                    onHealthChanged.Invoke(value);
                    health = value;
                }
            }
        }

        public void Damage()
        {
            Health--;
        }

        public UnityEvent<int> OnHealthChanged => onHealthChanged;

    }

}