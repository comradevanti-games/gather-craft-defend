using UnityEngine;

namespace GatherCraftDefend
{

    public static class Vector3Ext
    {

        public static Vector3 WithZ(this Vector3 v, float z) =>
            new Vector3(v.x, v.y, z);

    }

}