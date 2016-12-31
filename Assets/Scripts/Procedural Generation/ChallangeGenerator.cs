using UnityEngine;
using System.Collections;

public class ChallangeGenerator : MonoBehaviour
{
    public GameObject[] challenges;
    private Vector3 startPos;
    private Vector3 endPos;

    private void Awake()
    {
        startPos = transform.FindChild("StartPoint").transform.localPosition;
        endPos = transform.FindChild("EndPoint").transform.localPosition;
    }

    public void SpawnNew()
    {
        ChallangeGenerator newChallenge = GenerateRandomChallenges();
        newChallenge.transform.position = transform.position + endPos - newChallenge.startPos;
    }

    private ChallangeGenerator GenerateRandomChallenges()
    {
        GameObject newChallenge = Instantiate(challenges[Random.Range(0, challenges.Length)], transform.position, Quaternion.identity) as GameObject;
        return newChallenge.GetComponent<ChallangeGenerator>();
    }
}

