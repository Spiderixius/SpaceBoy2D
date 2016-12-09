using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceBetweenTwoObjects : MonoBehaviour {

    // Distance related
    public Transform object1;
    public Transform object2;
    public Text distanceText;
    private float distance;
    private Vector3 difference;
    float distanceInX;

    // Boss related
    public GameObject yellowBoss;
    public TheDestroyer theDestroyer;
    public float boostDuration;
    public float boostDurationLimit;
    private bool isBoostable;
    public float maxBossSpeed;
    public float minBossSpeed;
    public int boostCooldown;

    

	// Use this for initialization
	void Start () {
        //distance = 0;
        theDestroyer = yellowBoss.GetComponent("TheDestroyer") as TheDestroyer;
        isBoostable = true;
        StartCoroutine(SpeedUpBossCo());
    }
	
	// Update is called once per frame
	void Update () {
        if (object1 != null && object2 != null)
        {  
            SpeedUpBoss(distanceInX);
            calculateDistance(object1, object2);
            ChangeColourOfDistanceText(distanceText, distanceInX);
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
        distanceInX = Mathf.Abs(difference.x);
        distanceText.text = distanceInX.ToString("f0");  
    }

    /// <summary>
    /// The method speeds up the Yellow boss at certain times.
    /// </summary>
    /// <seealso cref="SpeedUpBossCo()"/>
    public void SpeedUpBoss(float distanceInX)
    {
        if (isBoostable)
        {
           
            boostDuration += Time.deltaTime;
            theDestroyer.speed = maxBossSpeed;
            if (boostDuration >= boostDurationLimit)
            {
                boostDuration = 0f;
                theDestroyer.speed = minBossSpeed;
                StartCoroutine(SpeedUpBossCo());
            }
        } else
        {
            theDestroyer.speed = minBossSpeed;
        }

    }

    /// <summary>
    /// A simple Co routine that waits an amount before making isBoostable to be true again.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpeedUpBossCo()
    {
        isBoostable = false;
        yield return new WaitForSeconds(boostCooldown);
        isBoostable = true;
    }


    /// <summary>
    /// A method to change size and color of the distance between the boss and player.
    /// </summary>
    /// <param name="distanceText"></param>
    /// <param name="distanceInX"></param>
    private void ChangeColourOfDistanceText(Text distanceText, float distanceInX)
    {
        if (distanceInX >= 30f)
        {
            distanceText.color = new Color(0f, 1f, 0f);
            distanceText.fontSize = 22;
        }
        else if(distanceInX >= 15f)
        {
            distanceText.color = new Color(255f / 128f, 87f / 255f, 0);
            distanceText.fontSize = 25;
        }
        else if(distanceInX < 15f)
        {
            distanceText.color = new Color(1f, 0f, 0f);
            distanceText.fontSize = 30;
        }
    }
}