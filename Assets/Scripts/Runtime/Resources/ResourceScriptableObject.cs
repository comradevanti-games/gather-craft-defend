using UnityEngine;

namespace GatherCraftDefend.Resources {

	[CreateAssetMenu(fileName = "Resource", menuName = "Resource/Add Resource...", order = 1)]
	public class ResourceScriptableObject : ScriptableObject {

		public string resourceName;
		public Sprite resourceSprite;
		public ResourceType resourceType;

	}

}