using UnityEngine;

namespace GatherCraftDefend
{

    public static class TimeProgression
    {

        public const float SecondsInDay = 30f;
        public const float Phase1End = SecondsInDay / 3f;
        public const float Phase2End = 2f * Phase1End;

        public static readonly GameRuntime initialTime = new GameRuntime(0);


        public static GameRuntime ProgressTimeBy(GameRuntime runtime, float seconds) =>
            new GameRuntime(runtime.TotalSeconds + seconds);

        public static int PassedDaysIn(GameRuntime runtime) =>
            Mathf.FloorToInt(runtime.TotalSeconds / SecondsInDay);

        public static float PassedDaySecondsIn(GameRuntime runtime) =>
            runtime.TotalSeconds % SecondsInDay;

        public static DayPhase DayPhaseOf(GameRuntime runtime) =>
            PassedDaySecondsIn(runtime) switch
            {
                <= Phase1End => DayPhase.Gather,
                <= Phase2End => DayPhase.Craft,
                _ => DayPhase.Defend
            };

        public static string Stringify(GameRuntime runtime)
        {
            var day = PassedDaysIn(runtime) + 1;
            var seconds = Mathf.FloorToInt(PassedDaySecondsIn(runtime));
            var phase = DayPhaseOf(runtime);
            return $"Day {day} - Seconds {seconds} ({phase})";
        }


        public record GameRuntime(float TotalSeconds);

    }

}