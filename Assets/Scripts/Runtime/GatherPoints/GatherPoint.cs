using GatherCraftDefend.Resources;
using UnityEngine;

namespace GatherCraftDefend.GatherPoints {

	public class GatherPoint : MonoBehaviour {

#region Fields

		[SerializeField] private ResourceType gatherPointResourceType;
		[SerializeField] private int maxGatherAmount;

#endregion

#region Properties

		private int GatherAmount { get; set; } = 0;

#endregion

#region Methods

		public void Gather() {
			if (GatherAmount < maxGatherAmount) {
				GatherAmount++;
				//TODO: What happens on a gather? Spawn Resources...
			}
			else {
				Destroy(gameObject);
			}
		}

#endregion

	}

}