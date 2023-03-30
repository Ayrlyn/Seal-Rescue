using System.Linq;
using UnityEngine;

public static class TransformExtensionMethods
{
    public static Transform Clear(this Transform transform)
    {
        var children = transform.Cast<Transform>().ToList();
        foreach (Transform child in children)
        {
            GameObject.Destroy(child.gameObject);
        }
        return transform;
    }
}