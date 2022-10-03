using System.Collections.Generic;
using UnityEngine;

namespace GatherCraftDefend {

	public class Salesman : MonoBehaviour {

#region Fields

		[SerializeField] private GameObject saleOpenGameObject;
		[SerializeField] private GameObject saleCloseGameObject;
		[SerializeField] private List<CraftingSlot> slots;

#endregion

		public void SwitchShopState(DayPhase phase) {

			if (phase == DayPhase.Craft) {
				saleCloseGameObject.SetActive(false);
				saleOpenGameObject.SetActive(true);
				ToggleSlots(true);
			}

			if (phase != DayPhase.Craft) {
				saleCloseGameObject.SetActive(true);
				saleOpenGameObject.SetActive(false);
				ToggleSlots(false);
			}

		}

		private void ToggleSlots(bool isOpen) {
			foreach (var slot in slots)
				slot.IsOpen = isOpen;
		}

	}

}