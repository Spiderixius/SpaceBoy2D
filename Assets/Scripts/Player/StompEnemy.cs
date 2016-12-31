using UnityEngine;
using System.Collections;

public class StompEnemy : MonoBehaviour {

    public int damageToGive;
    public float bounceForce;
    private Rigidbody2D thePlayerRigidBody;

    // Use this for initialization
    void Start () {
        thePlayerRigidBody = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            thePlayerRigidBody.velocity = new Vector3(thePlayerRigidBody.velocity.x, 
                bounceForce, 0f);
            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
        }
    }
}
