using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GatherCraftDefend.AmmoManagement;

namespace GatherCraftDefend.UI
{
    public class AmmoDisplay : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI label;


        public void OnAmmoBagChanged(AmmoBag bag)
        {
            label.text = $"/{bag.Bullets}";
        }

    }
}
