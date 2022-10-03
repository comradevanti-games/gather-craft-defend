using UnityEngine;
using UnityEngine.UI;
using static GatherCraftDefend.TimeProgression;

namespace GatherCraftDefend.UI
{

    public class DayNightDisplay : MonoBehaviour
    {

        [SerializeField] private Gradient overlayColorGradient;
        [SerializeField] private Image overlayImage;

        public void OnTimeChanged(GameRuntime runtime)
        {
            var seconds = PassedDaySecondsIn(runtime);
            var t = seconds / SecondsInDay;

            var color = overlayColorGradient.Evaluate(t);
            overlayImage.color = color;
        }

    }

}