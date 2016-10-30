using UnityEngine;
using System.Collections;

/// <summary>
/// Simple script to destroy gameObjects over time, 
/// for example particle effects and one time use spawners.
/// </summary>
public class DestroyOverTime : MonoBehaviour {

    public float lifeTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime = lifeTime - Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}
}
