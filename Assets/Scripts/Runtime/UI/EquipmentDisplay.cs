using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GatherCraftDefend.UI {

	public class EquipmentDisplay : MonoBehaviour {

		[SerializeField] private Image iconImage;
		[SerializeField] private List<EquipmentType> equipmentTypes;
		[SerializeField] private List<Sprite> equipmentSprites;

		private Dictionary<EquipmentType, Sprite> EquipmentIcons { get; } = new Dictionary<EquipmentType, Sprite>();

		private void Start() {
			for (int i = 0; i < equipmentTypes.Count; i++) {
				EquipmentIcons.Add(equipmentTypes[i], equipmentSprites[i]);
			}
		}

		public void OnEquipmentChanged(EquipmentType type) =>
			iconImage.sprite = EquipmentIcons[type];

	}

}