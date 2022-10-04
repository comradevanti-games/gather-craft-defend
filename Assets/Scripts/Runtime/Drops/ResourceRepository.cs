using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GatherCraftDefend.Resources {

	public class ResourceRepository : MonoBehaviour {

#region Fields

		[SerializeField] private List<ResourceScriptableObject> availableResources;
		[SerializeField] private GameObject resourcePrefab;
		[SerializeField] private Transform resourceContainer;

#endregion

#region Methods

		private ResourceScriptableObject GetResourceScriptableObject(ResourceType type) {
			return availableResources.First(resource => resource.resourceType == type);
		}

		private ResourceScriptableObject GetResourceScriptableObject(string resourceName) {
			return availableResources.First(resource => resource.resourceName == resourceName);
		}

		public void SpawnResourceAt(Vector2 gatherPointPosition, ResourceType resourceType) {
			var so = availableResources.Find(resourceSo => resourceSo.resourceType == resourceType);
			var resource = Instantiate(resourcePrefab, gatherPointPosition, Quaternion.identity, resourceContainer).GetComponent<Resource>();
			var dest = SpawnRing.GeneratePointAround(gatherPointPosition, 0.5f, 2.5f);
			resource.SpriteRenderer.sprite = so.resourceSprite;
			resource.ResourceType = so.resourceType;
			resource.Drop(dest);
		}



#endregion

	}

}