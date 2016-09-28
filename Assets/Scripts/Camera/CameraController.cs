using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public float followAhead;
    public float smoothing;

    public bool followTarget;

    private Vector3 targetPosition;

    // Camera bounds
    public bool bounds;

    public Vector3 minCameraPosition;
    public Vector3 maxCameraPostion;

    // Use this for initialization
    void Start()
    {
        followTarget = true;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowTarget(target, followAhead, smoothing, targetPosition);
    }


    private void FollowTarget(GameObject target, float followAhead, float smoothing, Vector3 targetPosition)
    {
        if (target != null)
        {
            if (followTarget)
            {
                targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

                // This moves the target of the camera ahead of the player
                if (target.transform.localScale.x > 0f)
                {
                    targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
                }
                else
                {
                    targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
                }

                // To avoid having different smoothing speeds for different frame rates, we will multiply with deltaTime.
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);

                if (bounds)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPostion.x),
                        Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPostion.y),
                        Mathf.Clamp(transform.position.z, minCameraPosition.z, maxCameraPostion.z));
                }
            }

        }
    }
}
