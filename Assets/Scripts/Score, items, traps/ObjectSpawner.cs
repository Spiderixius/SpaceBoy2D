using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

    public GameObject[] objectToSpawn;

	// Use this for initialization
	void Start () {
        SpawnObject();
	}

    public void SpawnObject()
    {
        GameObject newObject = GenerateRandomObject();
        newObject.gameObject.transform.position = transform.position;
    }

    private GameObject GenerateRandomObject()
    {

        if (objectToSpawn != null)
        {
            GameObject newObject = Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Length)], transform.position, Quaternion.identity) as GameObject;
            return newObject;
        }
        return null;
    }

}
