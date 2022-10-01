using UnityEngine;

namespace GatherCraftDefend.Resources {

	public class Resource : MonoBehaviour {

#region Fields

		[SerializeField] private SpriteRenderer spriteRenderer;

#endregion

#region Properties

		public ResourceType ResourceType { get; set; }

#endregion

	}

}