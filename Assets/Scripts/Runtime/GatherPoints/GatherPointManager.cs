using System;
using System.Collections.Generic;
using GatherCraftDefend.GatherPoints;
using GatherCraftDefend.Resources;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GatherCraftDefend {

	public class GatherPointManager : MonoBehaviour {

#region Fields

		[SerializeField] private ResourceRepository resourceRep;
		[SerializeField] private List<GameObject> gatherPointRepository;
		[SerializeField] private float minGatherPointDistance;
		[SerializeField] private float maxGatherPointDistance;
		[SerializeField] private int maxGatherPointsAmount;
		[SerializeField] private Transform gatherPointsContainer;

#endregion

#region Properties

		public List<GatherPoint> AvailableGatherPoints { get; set; }

#endregion

#region Methods

		private void Start() {
			AvailableGatherPoints = new List<GatherPoint>();
			SpawnGatherPoints(maxGatherPointsAmount);
		}

		private GameObject GetRandomGatherPoint() => gatherPointRepository[Random.Range(0, gatherPointRepository.Count - 1)];

		private void SpawnGatherPoints(int spawnAmount) {

			for (int i = 0; i <= spawnAmount; i++) {
				AvailableGatherPoints.Add(
					Instantiate(
							GetRandomGatherPoint(),
							SpawnRing.GeneratePoint(minGatherPointDistance, maxGatherPointDistance),
							Quaternion.identity)
						.GetComponent<GatherPoint>());
			}

		}

#endregion

	}

}