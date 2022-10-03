using UnityEngine;

namespace GatherCraftDefend
{

    public class PlaceablePlacer : MonoBehaviour
    {

        [SerializeField] private PlaceablePreview preview;
        [SerializeField] private Sprite barricadeSprite;

        private bool placerEnabled;

        public bool PlacerEnabled
        {
            get => placerEnabled;
            set
            {
                placerEnabled = value;
                if (placerEnabled)
                    preview.Show(barricadeSprite);
                else
                    preview.Hide();
            }
        }

        public void OnEquipmentChanged(EquipmentType equipmentType) =>
            PlacerEnabled = equipmentType == EquipmentType.WoodBarricade;

    }

}