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
			SpawnGatherPoints();
		}

		private GameObject GetRandomGatherPoint() => gatherPointRepository[Random.Range(0, gatherPointRepository.Count)];

		private void SpawnGatherPoints() {

			int currentGatherPointsAmount = AvailableGatherPoints.Count;
			if (currentGatherPointsAmount >= maxGatherPointsAmount) return;

			for (int i = currentGatherPointsAmount; i < maxGatherPointsAmount; i++) {

				var gatherPoint = Instantiate(
						GetRandomGatherPoint(),
						SpawnRing.GeneratePoint(minGatherPointDistance, maxGatherPointDistance),
						Quaternion.identity, gatherPointsContainer)
					.GetComponent<GatherPoint>();

				gatherPoint.onGatherPointExhausted += RemoveGatherPoint;
				gatherPoint.onGathered += Gather;
				AvailableGatherPoints.Add(gatherPoint);

			}

		}

		public void RespawnGatherPoints(DayPhase phase) {
			if (phase == DayPhase.Gather)
				SpawnGatherPoints();
		}

		private void Gather(GatherPoint gatherPoint) {
			resourceRep.SpawnResourceAt(SpawnRing.GeneratePointAround(gatherPoint.transform.position, 0.5f, 2f), gatherPoint.resourceType);
		}

		private void RemoveGatherPoint(GatherPoint gatherPoint) {
			gatherPoint.onGatherPointExhausted -= RemoveGatherPoint;
			gatherPoint.onGathered = Gather;
			AvailableGatherPoints.Remove(gatherPoint);
			Destroy(gatherPoint.gameObject);
		}

#endregion

	}

}