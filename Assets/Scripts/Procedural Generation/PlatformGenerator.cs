using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

    //public GameObject thePlatform;

    // Procedural generation related
    public Transform generationPoint;

    private float distanceBetween;
    //private float platformWidth;

    // Horizontal platform placement related
    public float distanceBetweenMin;
    public float distanceBetweenMax;
    //public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;

    // Vertical platform placement related
    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    // Object pooling related
    public ObjectPooler[] theObjectPools;

    // Coin related
    private CoinGenerator theCoinGenerator;
    public float randomCoinTreshold;

    // Spike related
    public float randomSpikeTreshold;
    public ObjectPooler spikepool;

    // Greenblob related
    private EnemyGenerator greenBlobGenerator;
    public float randomGreenblobTreshold;
    

	// Use this for initialization
	void Start () {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        
        platformWidths = new float[theObjectPools.Length];
        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();
        greenBlobGenerator = FindObjectOfType<EnemyGenerator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2f) + distanceBetween, heightChange, transform.position.z);

            //Instantiate(/*thePlatform*/ theObjectPools[platformSelector], transform.position, transform.rotation);

            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0f, 100f) < randomCoinTreshold)
            {
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z));

            }

            if (Random.Range(0f, 100f) < randomGreenblobTreshold)
            {
                greenBlobGenerator.SpawnBlob(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }

            if (Random.Range(0f, 100f) < randomSpikeTreshold)
            {
                //theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
                GameObject newSpike = spikepool.GetPooledObject();

                float spikeXPosition = Random.Range((-platformWidths[platformSelector] / 2f) + 2f, (platformWidths[platformSelector] / 2f) - 2f);

                Vector3 spikePosition = new Vector3(spikeXPosition, 1f, 0f);

                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2f), transform.position.y, transform.position.z);

        }
    }
}
