using System.Collections.Generic;
using ComradeVanti.CSharpTools;
using Dev.ComradeVanti;
using Dev.ComradeVanti.EnumDict;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GatherCraftDefend
{

    public class PlaceablesKeeper : MonoBehaviour
    {

        [SerializeField] private EnumDict<Placeable, GameObject> prefabs;
        [SerializeField] private Tilemap wallMap;

        private readonly HashSet<Vector2> takenPositions = new HashSet<Vector2>();


        public bool CanPlaceAt(Vector2 position)
        {
            var tilePosition = new Vector3Int(Mathf.FloorToInt(position.x),
                                              Mathf.FloorToInt(position.y), 0);
            return !takenPositions.Contains(position) &&
                   !wallMap.HasTile(tilePosition);
        }

        public void Place(Placeable placeable, Vector2 position)
        {
            var prefab = prefabs[placeable];
            var barricade = Instantiate(prefab, position, Quaternion.identity);
            takenPositions.Add(position);

            barricade.TryGetComponent<HealthKeeper>()
                     .Iter(it => it.OnHealthChanged.AddListener(health =>
                     {
                         if (health == 0)
                             takenPositions.Remove(position);
                     }));
        }

    }

}