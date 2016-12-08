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
        //StartCoroutine(SpeedUpBossCo());
    }
	
	// Update is called once per frame
	void Update () {
        if (object1 != null && object2 != null)
        {
            
            SpeedUpBoss(calculateDistance(object1, object2));
        }
    }

    /// <summary
    /// The method simply takes the difference of two objects and return the absolute value of x. 
    /// </summary>
    /// <param name="object1"></param>
    /// <param name="object2"></param>
    private float calculateDistance(Transform object1, Transform object2)
    {
        difference = object1.position - object2.position;

        float distanceInX = Mathf.Abs(difference.x);
        distanceText.text = distanceInX.ToString("f0");

        return distanceInX;
        
        
    }

    /// <summary>
    /// The method speeds up the Yellow boss at certain times.
    /// </summary>
    /// <seealso cref="SpeedUpBossCo()"/>
    public void SpeedUpBoss(float distanceInX)
    {
        if (isBoostable && distanceInX > 10 )
        {
           
            boostDuration += Time.deltaTime;
            theDestroyer.speed = maxBossSpeed;
            if (boostDuration >= boostDurationLimit)
            {
                boostDuration = 0;
                theDestroyer.speed = minBossSpeed;
                StartCoroutine(SpeedUpBossCo());
            }
       
        
        } else
        {
            theDestroyer.speed = minBossSpeed;
        }

    }

    public IEnumerator SpeedUpBossCo()
    {
        isBoostable = false;
        yield return new WaitForSeconds(boostCooldown);
        isBoostable = true;
    }
}