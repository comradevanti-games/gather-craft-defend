using Dev.ComradeVanti.EnumDict;
using UnityEngine;
using UnityEngine.UI;

namespace GatherCraftDefend.UI
{

    public class EquipmentDisplay : MonoBehaviour
    {

        [SerializeField] private Image iconImage;
        [SerializeField] private EnumDict<EquipmentType, Sprite> equipmentSprites;

        
        public void OnEquipmentChanged(EquipmentType type) =>
            iconImage.sprite = equipmentSprites[type];

    }

}