using UnityEngine;
using System.Collections;

public class ChallengeTriggerer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //GenerateRandomChallenge();
            gameObject.SetActive(true);
            GetComponentInParent<ChallangeGenerator>().SpawnNew();
            gameObject.SetActive(false);

        }
    }
}
