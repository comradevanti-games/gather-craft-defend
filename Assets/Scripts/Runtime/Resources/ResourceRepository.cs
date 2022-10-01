using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GatherCraftDefend.Resources {

	public class ResourceRepository : MonoBehaviour {

#region Fields

		[SerializeField] private List<ResourceScriptableObject> availableResources;
		[SerializeField] private GameObject resourcePrefab;

#endregion

#region Methods

		private ResourceScriptableObject GetResourceScriptableObject(ResourceType type) {
			return availableResources.First(resource => resource.resourceType == type);
		}

		private ResourceScriptableObject GetResourceScriptableObject(string resourceName) {
			return availableResources.First(resource => resource.resourceName == resourceName);
		}

		public Resource SpawnResourceAt(Vector2 position, ResourceType resourceType) {
			var so = availableResources.Find(resourceSo => resourceSo.resourceType == resourceType);
			var resource = Instantiate(resourcePrefab, position, Quaternion.identity).GetComponent<Resource>();
			resource.SpriteRenderer.sprite = so.resourceSprite;
			resource.ResourceType = so.resourceType;
			return resource;
		}

#endregion

	}

}