using UnityEngine;
using System.Collections;

public class GenerateChallenge : MonoBehaviour
{

    //public GameObject[] challenges;
    //Transform challengesSpawnPoint;
    //bool isGenerated = false;
    //public Vector3 startPoint, endPoint;

    // Use this for initialization
    void Start()
    {
        //challengesSpawnPoint = transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //void GenerateRandomChallenge()
    //{
    //    gameObject.SetActive(true);
    //    //GameObject newChallenge = Instantiate(challenges[Random.Range(0, challenges.Length)], challengesSpawnPoint.position, Quaternion.identity) as GameObject;
    //    GameObject newChallenge = Instantiate(challenges[Random.Range(0, challenges.Length)], transform.position, Quaternion.identity) as GameObject;
    //    newChallenge.transform.parent = null;
    //    isGenerated = true;
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //GenerateRandomChallenge();
            gameObject.SetActive(true);
            GetComponentInParent<Challange>().SpawnNew();
            gameObject.SetActive(false);
            //if (isGenerated)
            //{
                

            //}
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log(other.name);
    //    if (other.CompareTag("Player"))
    //    {
    //        if (!isGenerated && CompareTag("EndPointGround"))
    //        {
    //            GenerateRandomChallenge();
    //            GetComponentInParent<Challange>().SpawnNew();
    //        }
    //    }
    //}
}



//void GenerateRandomChallenge()
//{
//    GameObject newChallenge = Instantiate(challenges[Random.Range(0, challenges.Length)], transform.position, Quaternion.identity) as GameObject;
//    newChallenge.transform.parent = null;
//    isGenerated = true;
//}

//void OnTriggerEnter2D(Collider2D other)
//{
//    Debug.Log(other.name);
//    if (other.CompareTag("Player"))
//    {
//        if (!isGenerated && CompareTag("EndPointGround"))
//        {
//            GenerateRandomChallenge();
//        }