using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GatherCraftDefend.Resources {

	public class ResourcesBag : MonoBehaviour {

#region Events

		public UnityEvent<ResourceType, int> onResourceAddedToBag;
		public UnityEvent<ResourceType, int> onResourceRemovedFromBag;

#endregion

#region Fields

		[SerializeField] private AudioManager audioManager;

#endregion

#region Properties

		public Dictionary<ResourceType, int> Bag { get; set; }

#endregion

#region Methods

		private void Start() {
			Bag = new Dictionary<ResourceType, int>() {
				{ResourceType.Wood, 0},
				{ResourceType.CopperOre, 0},
				{ResourceType.IronOre, 0},
				{ResourceType.Berries, 0}
			};
		}

		public void AddResourceToBag(ResourceType resourceType) {
			Bag[resourceType] += 1;
			audioManager.PlayAudioClip("collect", gameObject);
			onResourceAddedToBag?.Invoke(resourceType, GetResourceAmount(resourceType));
		}

		public int GetResourceAmount(ResourceType resourceType) =>
			Bag[resourceType];

		public void RemoveFromResourceBag(ResourceType resourceType, int amount) {
			Bag[resourceType] -= amount;
			audioManager.PlayAudioClip("craft", gameObject);
			onResourceRemovedFromBag?.Invoke(resourceType, GetResourceAmount(resourceType));
		}

#endregion

	}

}