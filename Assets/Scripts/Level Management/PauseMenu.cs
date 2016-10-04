using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    public void PauseGame()
    {
        // Stops time to 0
        Time.timeScale = 0f;

        thePauseScreen.SetActive(true);
        theLevelManager.levelMusic.Pause();
        thePlayer.canMove = false;
    }

    public void ResumeGame()
    {
        thePauseScreen.SetActive(false);
        theLevelManager.levelMusic.UnPause();
        thePlayer.canMove = true;

        // Resumes game to normal speed
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("PlayerLives");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
