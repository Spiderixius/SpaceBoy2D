using UnityEngine;
using System.Collections;

/// <summary>
/// Discontinued.
/// </summary>
public class CameraShake : MonoBehaviour {

    public GameObject yellowBoss;
    public GameObject player;

    public float shakeTimer;
    public float shakeAmount;

    private float distanceBetweenPlayerAndBoss;


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
    void Update()
    {
        
        if (player && yellowBoss != null)
        {
            distanceBetweenPlayerAndBoss = player.transform.position.x - yellowBoss.transform.position.x;
            if (distanceBetweenPlayerAndBoss >= 0 || distanceBetweenPlayerAndBoss <= 50 )
            {
                if (shakeTimer >= 0)
                {
                    Vector2 shakePosition = Random.insideUnitCircle * shakeAmount;

                    transform.position = new Vector3(transform.position.x + shakePosition.x, transform.position.y + shakePosition.y, transform.position.z);

                    shakeTimer -= Time.deltaTime;
                }

                ShakeCamera(shakeAmount, shakeTimer);
            }
        }
    }

    public void ShakeCamera(float shakePower, float shakeDuration)
    {
        shakeAmount = shakePower;
        shakeTimer = shakeDuration;
    }
}
