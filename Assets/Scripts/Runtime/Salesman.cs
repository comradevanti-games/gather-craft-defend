using UnityEngine;

namespace GatherCraftDefend {

	public class Salesman : MonoBehaviour {

#region Fields

		[SerializeField] private GameObject saleOpenGameObject;
		[SerializeField] private GameObject saleCloseGameObject;

#endregion

		public void SwitchShopState(DayPhase phase) {

			if (phase == DayPhase.Craft) {
				saleCloseGameObject.SetActive(false);
				saleOpenGameObject.SetActive(true);
			}

			if (phase != DayPhase.Craft) {
				saleCloseGameObject.SetActive(true);
				saleOpenGameObject.SetActive(false);
			}

		}

	}

}