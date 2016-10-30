using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


/// <summary>
/// The game over code.
/// <see cref="Restart"/>
/// <seealso cref="QuitToMainMenu"/>
/// </summary>
public class GameOver : MonoBehaviour {

    public string mainMenu;
    public GameObject restartGameButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(restartGameButton);
    }

    /// <summary>
    /// A simple restarter which simply reloads the scene.
    /// </summary>
    public void Restart()
    {
        PlayerPrefs.DeleteKey("PlayerLives");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// A simple method to load the mainMenu scene.
    /// </summary>
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
