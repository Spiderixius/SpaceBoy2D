using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectPooler : MonoBehaviour {

    public GameObject pooledObject;

    public int pooledAmount;

    List<GameObject> pooledObjects;


	// Use this for initialization
	void Start () {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount ; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.parent = transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}

    /// <summary>
    /// Simply gets pooled objects from the pooled objects list.
    /// </summary>
    /// <returns></returns>
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.transform.parent = transform;
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

}
