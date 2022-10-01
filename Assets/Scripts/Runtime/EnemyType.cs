using UnityEngine;

namespace GatherCraftDefend
{

    [CreateAssetMenu(menuName = "GatherCraftDefend/Enemy-type", fileName = "New Enemy-type")]
    public class EnemyType : ScriptableObject
    {

        [SerializeField] private GameObject prefab;


        public GameObject Prefab => prefab;

    }

}