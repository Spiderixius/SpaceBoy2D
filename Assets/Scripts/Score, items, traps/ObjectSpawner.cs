using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public GameObject[] objectToSpawn;

	// Use this for initialization
	void Start () {
        SpawnObject();
	}

    public void SpawnObject()
    {
        GameObject newObject = GenerateRandomObject();

        if (newObject != null)
            newObject.gameObject.transform.position = transform.position;
    }

    private GameObject GenerateRandomObject()
    {

        if (objectToSpawn != null)
        {
            int typeToSpawn = Random.Range(0, 101);

            if (typeToSpawn < 5) { 
                ItemSpawner(4);
            }

            else if (typeToSpawn >= 5 && typeToSpawn < 10) { 
                ItemSpawner(3);
            }
            else if (typeToSpawn >= 10 && typeToSpawn < 20) { 
                ItemSpawner(2);
            }
            else if (typeToSpawn >= 20 && typeToSpawn < 40) { 
                ItemSpawner(1);
            }
            else if (typeToSpawn >= 40 && typeToSpawn < 80) { 
                ItemSpawner(0);
            }
            else { 
            return null;
            }
        }
        return null;
    }

    private GameObject ItemSpawner(int indexOfItem)
    {
        GameObject result = null;

        if (indexOfItem < objectToSpawn.Length)
        {
            result = Instantiate(objectToSpawn[indexOfItem], transform.position, Quaternion.identity) as GameObject;
        }

        return result;
    }

}

