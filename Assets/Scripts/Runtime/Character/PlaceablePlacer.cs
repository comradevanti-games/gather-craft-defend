using System.Collections.Generic;
using ComradeVanti.CSharpTools;
using Dev.ComradeVanti.EnumDict;
using UnityEngine;

namespace GatherCraftDefend
{

    public class PlaceablePlacer : MonoBehaviour
    {

        [SerializeField] private float maxPlaceDistance;
        [SerializeField] private PlaceablePreview preview;
        [SerializeField] private EnumDict<Placeable, Sprite> previewSprites;
        [SerializeField] private PlaceablesKeeper placeablesKeeper;


        private IOpt<Placeable> placeable = Opt.None<Placeable>();


        public Dictionary<Placeable, int> PlaceableCounts { get; } = new Dictionary<Placeable, int>
        {
            { GatherCraftDefend.Placeable.Barricade, 0 },
            { GatherCraftDefend.Placeable.Spikes, 0 }
        };

        private IOpt<Placeable> Placeable
        {
            get => placeable;
            set
            {
                placeable = value;
                placeable.Match(
                    it => preview.Show(previewSprites[it]),
                    () => preview.Hide());
            }
        }


        private void Update() =>
            placeable.Iter(it =>
            {
                if (Input.GetMouseButton(0))
                {
                    var position = preview.Position;
                    if (CanReach(position) && placeablesKeeper.CanPlaceAt(position) &&
                        PlaceableCounts[it] > 0)
                    {
                        placeablesKeeper.Place(it, position);
                        PlaceableCounts[it]--;
                    }
                }
            });


        private bool CanReach(Vector2 position) =>
            Vector2.Distance(position, transform.position) <= maxPlaceDistance;

        public void OnEquipmentChanged(EquipmentType equipmentType) =>
            Placeable = equipmentType switch
            {
                EquipmentType.WoodBarricade =>
                    Opt.Some(GatherCraftDefend.Placeable.Barricade),
                EquipmentType.IronSpikes =>
                    Opt.Some(GatherCraftDefend.Placeable.Spikes),
                _ => Opt.None<Placeable>()
            };

    }

}