using UnityEngine;

public static class QuaternionExtensionMethods
{

    public static float[] ToArray(this Quaternion quaternion)
    {
        return new float[] { quaternion.x, quaternion.y, quaternion.z, quaternion.w };
    }

    public static Quaternion ToQuaternion(this float[] array)
    {
        return new Quaternion(array[0], array[1], array[2], array[3]);
    }
}