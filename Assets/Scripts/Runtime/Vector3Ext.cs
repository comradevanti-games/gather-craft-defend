using UnityEngine;

namespace GatherCraftDefend
{

    public static class Vector3Ext
    {

        public static Vector3 WithZ(this Vector3 v, float z) =>
            new Vector3(v.x, v.y, z);

        public static Vector3 SnapToGrid(this Vector3 v) =>
            new Vector3(
                Mathf.Floor(v.x) + 0.5f,
                Mathf.Floor(v.y) + 0.5f, 0);

    }

}