using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// A script to handle the pausing of game.
/// </summary>
public class PauseMenu : MonoBehaviour {

    public string mainMenu;

    public GameObject thePauseScreen;


    private PlayerController thePlayer;
    private LevelManager theLevelManager;


	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        theLevelManager = FindObjectOfType<LevelManager>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }

    }

    /// <summary>
    /// The method to handle pausing of game, which also sets the timeScale to 0.
    /// By setting it to 0 the passing of time is stopped.
    /// </summary>
    public void PauseGame()
    {
        // Stops time to 0, this will ensure that no gameObject is in action.
        Time.timeScale = 0f;

        thePauseScreen.SetActive(true);
        theLevelManager.levelMusic.Pause();
        thePlayer.canMove = false;
    }

    /// <summary>
    /// The method to handle resuming of the game, which also sets the timeScale to 1.
    /// By setting it back to 1 the passing of time is resumed to normal speed (realtime).
    /// </summary>
    public void ResumeGame()
    {
        thePauseScreen.SetActive(false);
        theLevelManager.levelMusic.UnPause();
        thePlayer.canMove = true;

        // Resumes game to normal speed
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Simply reloads the scene and removes saved player preferences. 
    /// Time scale is returned to 1 here as well, as timeScale is application wide.
    /// </summary>
    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("PlayerLives");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Method to quit to the main menu. 
    /// </summary>
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
