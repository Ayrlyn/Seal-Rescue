using UnityEngine;

public static class VectorExtensionMethods
{
    public static float[] ToArray(this Vector3 v)
    {
        return new float[] { v.x, v.y, v.z };
    }

    public static Vector3 ToVector3(this float[] a)
    {
        return new Vector3(a[0], a[1], a[2]);
    }
}