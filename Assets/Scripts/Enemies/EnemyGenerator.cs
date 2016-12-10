using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {

    public ObjectPooler blobPool;

    public void SpawnBlob(Vector3 startPosition)
    {
        GameObject blob = blobPool.GetPooledObject();
        blob.transform.position = startPosition;
        blob.SetActive(true);
    }
}
