using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParallaxBackground : MonoBehaviour
{

    public RawImage[] backgrounds;
    public float smoothing;
    public float[] transitionSpeed;
    public float yReduction;

    private Vector3 previousCameraPosition;

    // Use this for initialization
    void Start()
    {
        previousCameraPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // How much movement do we need to happen
            Vector3 parallax = (previousCameraPosition - transform.position) * (transitionSpeed[i] / smoothing);

            // Adding the movement position to the current position

            float rectX = backgrounds[i].uvRect.x;
            float rectY = backgrounds[i].uvRect.y;
            backgrounds[i].uvRect = new Rect(rectX - (parallax.x * Time.deltaTime), rectY + ((parallax.y / yReduction) * Time.deltaTime), 1, 1);
        }

        previousCameraPosition = transform.position;
    }
}
