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

#endregion

	}

}