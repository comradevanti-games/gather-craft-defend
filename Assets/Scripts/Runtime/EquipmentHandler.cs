using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GatherCraftDefend
{
    public class EquipmentHandler : MonoBehaviour
    {

        [SerializeField] public UnityEvent<EquipmentType> onEquipmentChange;
        // Start is called before the first frame update
        private EquipmentType equipmentType;
        private float scroll = 0.1f;
        private float fluidIndex;
        private void Awake()
        {
           equipmentType = EquipmentType.Gun;
        }

        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
             fluidIndex = Mathf.Repeat(Input.mouseScrollDelta.y *  scroll+fluidIndex, 4) ;
             var newEquipment = (EquipmentType)Mathf.FloorToInt(fluidIndex);
             if (newEquipment != equipmentType)
             {
                 onEquipmentChange.Invoke(newEquipment);
                 equipmentType = newEquipment;
                 
             }
        }

        
    }
}
