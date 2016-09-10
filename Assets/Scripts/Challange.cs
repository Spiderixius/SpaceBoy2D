using UnityEngine;
using System.Collections;

public class Challange : MonoBehaviour
{
    public GameObject[] challenges;
    private Vector3 startPos;
    private Vector3 endPos;

    private void Awake()
    {
        this.startPos = this.transform.FindChild("StartPoint").transform.localPosition;
        this.endPos = this.transform.FindChild("EndPoint").transform.localPosition;
    }

    public void SpawnNew()
    {
        Challange newChallenge = GenerateRandomChallenges();
        newChallenge.transform.position = this.transform.position + this.endPos - newChallenge.startPos;
    }

    private Challange GenerateRandomChallenges()
    {
        GameObject newChallenge = Instantiate(challenges[Random.Range(0, challenges.Length)], transform.position, Quaternion.identity) as GameObject;
        return newChallenge.GetComponent<Challange>();
    }
}

