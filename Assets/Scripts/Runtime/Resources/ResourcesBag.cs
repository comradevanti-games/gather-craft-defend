using System.Collections.Generic;
using System.Linq;
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

		public List<ResourceType> Bag { get; set; }

#endregion

#region Methods

		private void Start() {
			Bag = new List<ResourceType>();
		}

		public void AddResourceToBag(ResourceType resourceType) {
			Bag.Add(resourceType);
			audioManager.PlayAudioClip("collect", gameObject);
			onResourceAddedToBag?.Invoke(resourceType, GetResourceAmount(resourceType));
		}

		public int GetResourceAmount(ResourceType resourceType) =>
			Bag.FindAll(resType => resType == resourceType).Count;

		public void RemoveFromResourceBag(ResourceType resourceType, int amount) {

			for (int i = 0; i < amount; i++) {
				Bag.Remove(Bag.First(resType => resType == resourceType));
			}

			onResourceRemovedFromBag?.Invoke(resourceType, GetResourceAmount(resourceType));
		}

#endregion

	}

}