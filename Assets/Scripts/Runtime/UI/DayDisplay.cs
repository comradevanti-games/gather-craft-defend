using TMPro;
using UnityEngine;
using static GatherCraftDefend.TimeProgression;

namespace GatherCraftDefend.UI
{

    public class DayDisplay : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI dayLabel;


        public void OnTimeChanged(GameRuntime runtime)
        {
            var day = PassedDaysIn(runtime) + 1;
            UpdateDisplay(day);
        }

        private void UpdateDisplay(int day) =>
            dayLabel.text = day.ToString();

    }

}