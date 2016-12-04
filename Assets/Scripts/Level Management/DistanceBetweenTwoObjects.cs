using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceBetweenTwoObjects : MonoBehaviour {

    public Transform object1;
    public Transform object2;
    public Text distanceText;
    private float distance;
    private Vector3 difference;

	// Use this for initialization
	void Start () {
        //distance = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (object1 != null && object2 != null)
        {
            calculateDistance(object1, object2);
        }
    }

    /// <summary
    /// The method simply takes the difference of two objects and return the absolute value of x. 
    /// </summary>
    /// <param name="object1"></param>
    /// <param name="object2"></param>
    private void calculateDistance(Transform object1, Transform object2)
    {
        difference = object1.position - object2.position;

        var distanceInX = Mathf.Abs(difference.x);
        distanceText.text = distanceInX.ToString("f0");
    }
}
