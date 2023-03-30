using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAtlas : Singleton<TransformAtlas>
{
    #region local variables
    Dictionary<string, List<Collider>> transformDictionary = new Dictionary<string, List<Collider>>();
    #endregion

    #region unity methods
    void Start()
    {
        RegisterToDictionary(FindObjectsOfType<Collider>());
    }
    #endregion

    #region local methods
    void RegisterToDictionary(Collider[] colliders)
    {
        if (colliders == null || colliders.Length == 0) { return; }

        foreach (Collider collider in colliders)
        {
            if (transformDictionary.ContainsKey(collider.tag)) { transformDictionary[collider.tag].Add(collider); }
            else { transformDictionary.Add(collider.tag, new List<Collider>() { collider }); }
        }
    }
    #endregion

    #region public methods
    public List<Transform> GetTargetTransformsByTag(string tag, Vector3 origin, float radius)
    {
        List<Transform> targets = new List<Transform>();
        if (string.IsNullOrEmpty(tag) || !transformDictionary.ContainsKey(tag)) { return targets; }

        List<Collider> allPossibleTargets = transformDictionary[tag];

        foreach (Collider targetCollider in allPossibleTargets)
        {
            if (targetCollider != null &&
              targetCollider.gameObject.activeInHierarchy &&
              targetCollider.enabled &&
              (Vector3.Distance(targetCollider.ClosestPoint(origin), origin) <= radius))
            {
                targets.Add(targetCollider.transform);
            }
        }

        return targets;
    }

    public void Register(GameObject gameObject)
    {
        if (gameObject == null) { return; }

        RegisterToDictionary(gameObject.GetComponentsInChildren<Collider>());
    }
    #endregion
}
