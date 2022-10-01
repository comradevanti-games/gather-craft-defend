using UnityEngine;
using UnityEngine.Events;

namespace GatherCraftDefend
{

    public class TriggerPoint : MonoBehaviour
    {

        [SerializeField] private UnityEvent<GameObject> onEntered;


        private void OnTriggerEnter2D(Collider2D col) =>
            onEntered.Invoke(col.gameObject);

    }

}