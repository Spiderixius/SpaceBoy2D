using UnityEngine;
using System.Collections;

/// <summary>
/// Code for the enemy health.
/// </summary>
public class EnemyHealthManager : MonoBehaviour {

    private int enemyHealth;
    public int maxHealth;
    public GameObject deathSplosionEffect;

	// Use this for initialization
	void Start () {
        enemyHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyHealth <= 0)
        {
            Instantiate(deathSplosionEffect, transform.position, transform.rotation);
            //Destroy(gameObject);
            gameObject.SetActive(false);
            enemyHealth = maxHealth;
        }
	}

    public void giveDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
    }
}
