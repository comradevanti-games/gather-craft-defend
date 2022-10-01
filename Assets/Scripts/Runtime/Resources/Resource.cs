using UnityEngine;

namespace GatherCraftDefend.Resources {

	public class Resource : MonoBehaviour {

#region Properties

		[field: SerializeField]
		public SpriteRenderer SpriteRenderer { get; set; }

		public ResourceType ResourceType { get; set; }

#endregion

#region Methods

		public void Collect() {
			Destroy(gameObject);
		}

#endregion

	}

}