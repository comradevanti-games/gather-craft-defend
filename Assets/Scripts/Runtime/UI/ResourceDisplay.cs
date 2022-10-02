using System.Collections.Generic;
using GatherCraftDefend.Resources;
using TMPro;
using UnityEngine;

namespace GatherCraftDefend.UI {

	public class ResourceDisplay : MonoBehaviour {

#region Fields

		[SerializeField] private TextMeshProUGUI woodMesh;
		[SerializeField] private TextMeshProUGUI copperMesh;
		[SerializeField] private TextMeshProUGUI ironMesh;
		[SerializeField] private TextMeshProUGUI meatMesh;

		private Dictionary<ResourceType, TextMeshProUGUI> resourceDisplayDictionary;

#endregion

#region Methods

		private void Start() {
			resourceDisplayDictionary = new Dictionary<ResourceType, TextMeshProUGUI>() {
				{ResourceType.Wood, woodMesh},
				{ResourceType.CopperOre, copperMesh},
				{ResourceType.IronOre, ironMesh},
				{ResourceType.Meat, meatMesh},

			};
		}

		public void OnResourceAddedToBag(ResourceType resType, int amount) {
			resourceDisplayDictionary[resType].text = amount.ToString();
		}

#endregion

	}

}