using UnityEngine;
using UnityEngine.Events;

namespace GatherCraftDefend
{

    public class EquipmentHandler : MonoBehaviour
    {

        [SerializeField] private UnityEvent<EquipmentType> onEquipmentChange;

        private EquipmentType equipmentType = EquipmentType.Gun;
        private float fluidIndex;


        private void Update()
        {
            var input = Input.mouseScrollDelta.y;
            fluidIndex = Mathf.Repeat(fluidIndex + input, 4);

            var newEquipmentType = (EquipmentType)Mathf.FloorToInt(fluidIndex);
            if (newEquipmentType != equipmentType)
            {
                onEquipmentChange.Invoke(newEquipmentType);
                equipmentType = newEquipmentType;
            }
        }

    }

}