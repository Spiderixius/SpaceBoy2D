using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour {

    public  float distanceTraveled;
    public Text distanceText;

	// Use this for initialization
	void Start () {
        distanceTraveled = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x >= distanceTraveled)
        {
            distanceTraveled = transform.position.x;
            distanceText.text = distanceTraveled.ToString("f0");
        }
	}
}
