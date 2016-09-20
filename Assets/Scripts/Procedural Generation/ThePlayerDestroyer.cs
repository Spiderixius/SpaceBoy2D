using UnityEngine;
using System.Collections;

public class ThePlayerDestroyer : MonoBehaviour {

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
            Destroy(other.gameObject);
        }
    }
}
