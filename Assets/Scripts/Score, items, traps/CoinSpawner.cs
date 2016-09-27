using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour {

    public GameObject[] coins;


	// Use this for initialization
	void Start () {
        SpawnCoin();
	}
	
    public void SpawnCoin()
    {
        
        GameObject newCoin = GenerateRandomCoin();
        newCoin.gameObject.transform.position = transform.position;
        
        
    }

    private GameObject GenerateRandomCoin()
    {
        GameObject newCoin = Instantiate(coins[Random.Range(0, coins.Length)], transform.position, Quaternion.identity) as GameObject;
        return newCoin;
    }
}
