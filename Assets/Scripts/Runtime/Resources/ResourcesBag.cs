using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GatherCraftDefend.Resources {

	public class ResourcesBag : MonoBehaviour {

#region Events

		public UnityEvent<ResourceType> onResourceAddedToBag;
		public UnityEvent<ResourceType, int> onResourceRemovedFromBag;

#endregion

#region Properties

		public List<Resource> Bag { get; set; }

#endregion

#region Methods

		private void Start() {
			Bag = new List<Resource>();
		}

		public void AddResourceToBag(Resource resource) {
			Bag.Add(resource);
			onResourceAddedToBag?.Invoke(resource.ResourceType);
		}

		public int GetResourceAmount(ResourceType resourceType) =>
			Bag.FindAll(resource => resource.ResourceType == resourceType).Count;

		public void RemoveFromResourceBag(ResourceType resourceType, int amount) {
			Bag.Remove(Bag.First(resource => resource.ResourceType == resourceType));
			onResourceRemovedFromBag?.Invoke(resourceType, amount);
		}

#endregion

	}

}