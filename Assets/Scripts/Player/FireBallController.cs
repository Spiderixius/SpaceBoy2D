using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {

    public float speed;
    public Rigidbody2D myRigidbody;
    public PlayerController player;
    public GameObject impactEffect;
    public float zRotation;

    

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
        myRigidbody = GetComponent<Rigidbody2D>();

        if (player.transform.localScale.x < 0)
        {
            speed = -speed;
            if (zRotation != 0)
            {
                transform.Rotate(new Vector3(0, 0, zRotation * Time.deltaTime));
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
    }

    void FixedUpdate()
    {
        if (player.transform.localScale.x < 0)
        {
            if (zRotation != 0)
            {
                transform.Rotate(new Vector3(0, 0, zRotation * Time.deltaTime));
                transform.localScale = new Vector3(-0.5241966f, 0.5241966f, 0.5241966f);
            }
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -zRotation * Time.deltaTime));
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
