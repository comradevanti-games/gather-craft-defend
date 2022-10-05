using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GatherCraftDefend
{

    public class PlaceablesKeeper : MonoBehaviour
    {
        
        [SerializeField] private Tilemap wallMap;
        
        [SerializeField] private List<Placeable> placeables;
        [SerializeField] private List<GameObject> prefabs;

        private readonly HashSet<Vector2> takenPositions = new HashSet<Vector2>();

        private Dictionary<Placeable, GameObject> PlaceablePrefabs { get; } = new Dictionary<Placeable, GameObject>();

        private void Start() {
            for(int i= 0; i<placeables.Count; i++)
                PlaceablePrefabs.Add(placeables[i], prefabs[i]);
        }

        public bool CanPlaceAt(Vector2 position)
        {
            var tilePosition = new Vector3Int(Mathf.FloorToInt(position.x),
                                              Mathf.FloorToInt(position.y), 0);
            return !takenPositions.Contains(position) &&
                   !wallMap.HasTile(tilePosition);
        }

        public void Place(Placeable placeable, Vector2 position)
        {
            var prefab = PlaceablePrefabs[placeable];
            var barricade = Instantiate(prefab, position, Quaternion.identity);
            takenPositions.Add(position);
            
            /*

            barricade.TryGetComponent<HealthKeeper>()
                     .Iter(it => it.OnHealthChanged.AddListener(health =>
                     {
                         if (health == 0)
                             takenPositions.Remove(position);
                     }));
                     */
        }

    }

}