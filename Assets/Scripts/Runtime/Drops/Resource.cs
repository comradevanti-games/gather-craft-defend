using System.Collections;
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

		public void Drop(Vector2 destination) {
			StartCoroutine(DropToPosition(destination));
		}

		private IEnumerator DropToPosition(Vector2 destination) {
			
			while (Vector2.Distance(transform.position, destination) > 0.005f) {
				transform.position = Vector2.MoveTowards(transform.position, destination, 5 * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}

			transform.position = destination;

		}

#endregion

	}

}