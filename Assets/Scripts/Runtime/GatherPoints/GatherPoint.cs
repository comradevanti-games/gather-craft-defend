using GatherCraftDefend.Resources;
using UnityEngine;

namespace GatherCraftDefend.GatherPoints {

	public class GatherPoint : MonoBehaviour {

#region Fields

		[SerializeField] private ResourceType gatherPointResourceType;

#endregion

#region Properties

#endregion

#region Methods

		public void Gather() {
			Debug.Log("I got gathered!");
		}

#endregion

	}

}