using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GatherCraftDefend
{

    public class PlaceablesKeeper : MonoBehaviour
    {

        [SerializeField] private GameObject barricadePrefab;
        [SerializeField] private Tilemap wallMap;

        private readonly HashSet<Vector2> takenPositions = new HashSet<Vector2>();


        public bool CanPlaceAt(Vector2 position)
        {
            var tilePosition = new Vector3Int(Mathf.FloorToInt(position.x), 
                                              Mathf.FloorToInt(position.y), 0);
            return !takenPositions.Contains(position) &&
                   !wallMap.HasTile(tilePosition);
        }

        public void PlaceBarricadeAt(Vector2 position)
        {
            var barricade = Instantiate(barricadePrefab, position, Quaternion.identity);
            takenPositions.Add(position);

            barricade.GetComponent<HealthKeeper>()
                     .OnHealthChanged.AddListener(health =>
                     {
                         if (health == 0)
                             takenPositions.Remove(position);
                     });
        }

    }

}