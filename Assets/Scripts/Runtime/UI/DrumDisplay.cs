using UnityEngine;
using static GatherCraftDefend.AmmoManagement;

namespace GatherCraftDefend.UI
{

    public class DrumDisplay : MonoBehaviour
    {

        [SerializeField] private GameObject[] bulletBackObjects;

        public void OnDrumChanged(Drum drum) =>
            bulletBackObjects.IterI((i, g) => g.SetActive(i < drum.Bullets));

    }

}