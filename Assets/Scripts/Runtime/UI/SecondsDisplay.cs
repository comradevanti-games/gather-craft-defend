using UnityEngine;
using UnityEngine.UI;
using static GatherCraftDefend.TimeProgression;

namespace GatherCraftDefend.UI
{

    public class SecondsDisplay : MonoBehaviour
    {

        [SerializeField] private Sprite[] pointerSprites;
        [SerializeField] private Image pointerImage;


        public void OnTimeChanged(GameRuntime runtime)
        {
            var seconds = PassedPhaseSecondsIn(runtime);
            UpdateClock(seconds);
        }

        private void UpdateClock(float seconds)
        {
            var t = seconds / SecondsInPhase;
            var index = Mathf.FloorToInt(t * pointerSprites.Length);
            pointerImage.sprite = pointerSprites[index];
        }

    }

}