using UnityEngine;

namespace GatherCraftDefend
{

    public static class SpawnRing
    {

        private static float RandomAngle() =>
            Random.Range(0, 360f) * Mathf.Deg2Rad;

        private static float RandomDistance(float min, float max) =>
            Mathf.Sqrt(Random.Range(0, max * max - min * min) + min * min);

        public static Vector2 GeneratePoint(float minDistance, float maxDistance)
        {
            var theta = RandomAngle();
            var distance = RandomDistance(minDistance, maxDistance);

            return new Vector2(
                distance * Mathf.Cos(theta),
                distance * Mathf.Sin(theta));
        }

    }

}