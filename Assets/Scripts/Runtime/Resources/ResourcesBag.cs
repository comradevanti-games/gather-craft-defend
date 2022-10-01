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

		public List<ResourceType> Bag { get; set; }

#endregion

#region Methods

		private void Start() {
			Bag = new List<ResourceType>();
		}

		public void AddResourceToBag(ResourceType resourceType) {
			Bag.Add(resourceType);
			onResourceAddedToBag?.Invoke(resourceType);
		}

		public int GetResourceAmount(ResourceType resourceType) =>
			Bag.FindAll(resType => resType == resourceType).Count;

		public void RemoveFromResourceBag(ResourceType resourceType, int amount) {
			Bag.Remove(Bag.First(resType => resType == resourceType));
			onResourceRemovedFromBag?.Invoke(resourceType, amount);
		}

#endregion

	}

}