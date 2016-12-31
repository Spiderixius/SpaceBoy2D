using UnityEngine;
using System.Collections;


/// <summary>
/// Code for the green blob, which mostly focuses on the movement of the green blob,
/// which includes edge/wall detection and turning when an edge/wall has been detected.
/// </summary>
public class GreenBlob : MonoBehaviour {

    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D myBody;
    Transform myTransform;
    float myWidth, myHeight;


	// Use this for initialization
	void Start () {
        myTransform = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // Check to see if there's ground in front of us before moving forward.
        Vector2 lineCastPos = myTransform.position.toVector2() - myTransform.right.toVector2() * myWidth + Vector2.up * myHeight;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

        // Checks to see if there is something blocking us. 
        Debug.DrawLine(lineCastPos, lineCastPos - myTransform.right.toVector2() * .05f);
        bool isblocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTransform.right.toVector2() * .05f, enemyMask);

        // If there's no ground, turn around. Or if blob is blocked.
        if (!isGrounded || isblocked)
        {
            Vector3 currentRotation = myTransform.eulerAngles;
            currentRotation.y += 180;
            myTransform.eulerAngles = currentRotation;
        }

        // Always move forward
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTransform.right.x * speed;
        myBody.velocity = myVel;
	}
}
