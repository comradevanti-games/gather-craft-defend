using UnityEngine;

namespace GatherCraftDefend
{

    public class PlaceablePlacer : MonoBehaviour
    {

        [SerializeField] private PlaceablePreview preview;
        [SerializeField] private Sprite barricadeSprite;
        [SerializeField] private PlaceablesKeeper placeablesKeeper;

        private bool placerEnabled;


        private bool PlacerEnabled
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

        private void Update()
        {
            if (PlacerEnabled && Input.GetMouseButton(0))
            {
                var position = preview.Position;
                if (placeablesKeeper.CanPlaceAt(position))
                    placeablesKeeper.PlaceBarricadeAt(position);
            }
        }


        public void OnEquipmentChanged(EquipmentType equipmentType) =>
            PlacerEnabled = equipmentType == EquipmentType.WoodBarricade;

    }

}