using UnityEngine;

namespace GatherCraftDefend
{

    public class PlaceablePlacer : MonoBehaviour
    {

        [SerializeField] private float maxPlaceDistance;
        [SerializeField] private PlaceablePreview preview;
        [SerializeField] private Sprite barricadeSprite;
        [SerializeField] private PlaceablesKeeper placeablesKeeper;

        private bool placerEnabled;


        public int BarricadeCount { get; set; }

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
                if (CanReach(position) && placeablesKeeper.CanPlaceAt(position)
                                       && BarricadeCount > 0)
                {
                    placeablesKeeper.PlaceBarricadeAt(position);
                    BarricadeCount--;
                }
            }
        }


        private bool CanReach(Vector2 position) =>
            Vector2.Distance(position, transform.position) <= maxPlaceDistance;

        public void OnEquipmentChanged(EquipmentType equipmentType) =>
            PlacerEnabled = equipmentType == EquipmentType.WoodBarricade;

    }

}