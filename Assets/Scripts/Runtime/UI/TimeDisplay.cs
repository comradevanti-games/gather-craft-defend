using TMPro;
using UnityEngine;
using static GatherCraftDefend.TimeProgression;

namespace GatherCraftDefend.UI
{

    public class TimeDisplay : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI text;


        public void OnTimeChanged(GameRuntime runtime) => 
            text.text = Stringify(runtime);

    }

}