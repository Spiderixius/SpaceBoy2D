using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


/// <summary>
/// Code for the restarter, which simply calls the Respawn method 
/// if the player triggers this script, which the gameObject it is attached to.
/// <seealso cref="LevelManager.Respawn"/>
/// </summary>
public class Restarter : MonoBehaviour {

    public LevelManager theLevelManager;

	// Use this for initialization
	void Start () {
        theLevelManager = FindObjectOfType<LevelManager>();
	}

    /// <summary>
    /// A trigger method, where if other gameObject equals to Player, 
    /// then the respawn method is called.
    /// </summary>
    /// <param name="other">Indicates a seperate gameObject's 2D collider, 
    /// that is not the gameObject's which this script is attached to.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            theLevelManager.Respawn();
        }
    }
}
