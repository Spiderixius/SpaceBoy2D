using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Restarter : MonoBehaviour {

    public string levelToLoad;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        //Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        //rb.isKinematic = false;

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
