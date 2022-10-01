using UnityEngine;

namespace GatherCraftDefend.Resources {

	public class Resource : MonoBehaviour {

#region Fields

		[SerializeField] private SpriteRenderer spriteRenderer;

#endregion

#region Properties

		public ResourceType ResourceType { get; set; }

#endregion

#region Methods

		public void Collect() {
			Destroy(gameObject);
		}

#endregion

	}

}