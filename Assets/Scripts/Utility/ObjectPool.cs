using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    #region serializable properties
    [SerializeField] int _minimumObjectsInPool = 5;
    [SerializeField] int _maximumNumberObjectsInPool = 100;
    #endregion

    #region local properties
    Dictionary<GameObject, List<GameObject>> poolDictionary = new Dictionary<GameObject, List<GameObject>>();
    #endregion

    #region local methods
    GameObject CreateNewObject(GameObject g, int i)
    {
        GameObject gameObject = Instantiate(g, Vector3.zero, Quaternion.identity) as GameObject;
        gameObject.name += " " + i;
        gameObject.transform.parent = this.transform;
        gameObject.SetActive(false);
        return gameObject;
    }

    void CreateNewObjectPool(GameObject key)
    {
        List<GameObject> objects = new List<GameObject>();

        for (int i = 0; i < _minimumObjectsInPool; i++)
        {
            objects.Add(CreateNewObject(key, i));
        }

        poolDictionary.Add(key, objects);
    }

    GameObject GetGameObjectFromPool(List<GameObject> objectList)
    {
        bool removeThisFromPool = false;
        GameObject gameObject = null;
        foreach (GameObject g in objectList)
        {
            if (g == null || g.Equals(null))
            {
                Debug.Log("Null object found in list.");
                removeThisFromPool = true;
            }
            else if (!g.activeInHierarchy)//If the game object, g, is not active then it is available for use
            {
                gameObject = g;
                break;
            }
        }

        if (removeThisFromPool)
        {
            RemoveNullObjects(objectList);
        }

        if (gameObject != null)
        {
            return gameObject;
        }
        //If we made it here, there are no spare inactive game objects we can use
        if (objectList.Count >= _maximumNumberObjectsInPool)
        {
            return null;
        }
        GameObject go = CreateNewObject(objectList[0], objectList.Count);
        objectList.Add(go);
        return go;
    }

    void RemoveNullObjects(List<GameObject> listWithNullObjects)
    {
        Debug.Log("Removing null objects from the pool.");
        listWithNullObjects.RemoveAll(n => n == null || n.Equals(null));
    }
    #endregion

    #region public methods
    /// <summary>
    /// Remove null / destroyed object from list
    /// </summary>
    public void CleanPool()
    {
        List<GameObject> listCleanObject = new List<GameObject>();
        foreach (var keyPairPool in poolDictionary)
        {
            RemoveNullObjects(keyPairPool.Value);
            if (keyPairPool.Value.Count == 0)
            {
                listCleanObject.Add(keyPairPool.Key);
            }
        }

        foreach (var listedObj in listCleanObject)
        {
            poolDictionary.Remove(listedObj);
        }
    }

    /// <summary>
    /// Desotroy all object cached in pool
    /// </summary>
    public void ClearPool()
    {
        foreach (var keyPairPool in poolDictionary)
        {
            var listOfObject = keyPairPool.Value;
            int countObj = listOfObject.Count;
            for (int i = countObj - 1; i >= 0; i--)
            {
                if (listOfObject[i] == null || listOfObject[i].Equals(null)) continue; //skip null or destroyed object

                if (Application.isPlaying)
                {
                    Destroy(listOfObject[i]);
                }
                else
                {
                    DestroyImmediate(listOfObject[i]);
                }
            }
        }
        poolDictionary.Clear();
    }

    public GameObject GetGameObject(GameObject key)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            CreateNewObjectPool(key);
        }
        return GetGameObjectFromPool(poolDictionary[key]);
    }
    #endregion
}
