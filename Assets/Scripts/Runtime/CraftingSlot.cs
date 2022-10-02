using GatherCraftDefend.Resources;
using UnityEngine;

namespace GatherCraftDefend {

	public class CraftingSlot : MonoBehaviour {

#region Fields

		[SerializeField] private ResourceType priceType;
		[SerializeField] private int price;
		[SerializeField] private GameObject priceTagPrefab;
		[SerializeField] private Sprite priceTagSprite;
		[SerializeField] private Vector2 priceTagOffset;
		[SerializeField] private CraftingType craftingType;

#endregion

#region Properties

		public ResourceType PriceType => priceType;

		public int Price => price;

		public CraftingType CraftingType => craftingType;

#endregion

#region Methods

		private void Start() {
			FillPriceTag();
		}

		private void FillPriceTag() {
			for (int i = 0; i < price; i++) {
				var priceSymbol = Instantiate(priceTagPrefab,
					new Vector2(transform.position.x + priceTagOffset.x + i * 0.15f, transform.position.y + priceTagOffset.y),
					Quaternion.identity,
					transform);
				priceSymbol.GetComponent<SpriteRenderer>().sprite = priceTagSprite;
			}
		}

#endregion

	}

}