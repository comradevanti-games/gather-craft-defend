using UnityEngine;
using UnityEngine.Events;
using static GatherCraftDefend.TimeProgression;

namespace GatherCraftDefend
{

    public class TimeKeeper : MonoBehaviour
    {

        [SerializeField] private UnityEvent<GameRuntime> onTimeChanged;
        [SerializeField] private UnityEvent<DayPhase> onPhaseChanged;

        private GameRuntime runtime = initialTime;


        private void Update() =>
            ProgressTime();

        private void ProgressTime()
        {
            var newTime = ProgressTimeBy(runtime, Time.deltaTime);
            OnTimeChanged(newTime);
        }

        private void OnTimeChanged(GameRuntime newTime)
        {
            if (DayPhaseOf(newTime) != DayPhaseOf(runtime))
                onPhaseChanged.Invoke(DayPhaseOf(newTime));
            
            onTimeChanged.Invoke(newTime);
            runtime = newTime;
        }

    }

}