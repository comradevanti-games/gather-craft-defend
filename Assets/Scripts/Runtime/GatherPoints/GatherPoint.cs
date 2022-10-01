using System;
using GatherCraftDefend.Resources;
using UnityEngine;

namespace GatherCraftDefend.GatherPoints {

	public class GatherPoint : MonoBehaviour {

#region Fields

		[SerializeField] private int maxGatherAmount;

		public ResourceType resourceType;
		public Action<GatherPoint> onGatherPointExhausted;
		public Action<GatherPoint> onGathered;

#endregion

#region Properties

		private GatherPointManager GatherPointManager { get; set; }
		private int GatherAmount { get; set; } = 0;

#endregion

#region Methods

		public void Gather() {
			if (GatherAmount < maxGatherAmount) {
				GatherAmount++;
				onGathered?.Invoke(this);
			}
			else {
				onGatherPointExhausted?.Invoke(this);
			}
		}

#endregion

	}

}