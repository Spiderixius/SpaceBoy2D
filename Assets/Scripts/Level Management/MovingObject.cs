using UnityEngine;
using System.Collections;

/// <summary>
/// Script to handle movement of any object that this script is attached to.
/// </summary>
public class MovingObject : MonoBehaviour {

    public GameObject objectToMove;

    // Two point movement variables.
    public Transform startPoint;
    public Transform endPoint;

    public float moveSpeed;

    // The current point the gameObject is moving towards.
    private Vector3 currentTarget;

	// Use this for initialization
	void Start () {
        currentTarget = endPoint.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (objectToMove != null)   
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);

            if (objectToMove.transform.position == endPoint.position)
            {
                currentTarget = startPoint.position;
            }

            if (objectToMove.transform.position == startPoint.position)
            {
                currentTarget = endPoint.position;
            }
        }
        
	}

    /// <summary>
    /// For debugging purposes, simply draws a white box around start and end points. 
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(startPoint.position, transform.localScale);
        Gizmos.DrawWireCube(endPoint.position, transform.localScale);
    }
}
