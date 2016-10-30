using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// The code to handle the main menu scene.
/// <see cref="NewGame"/>
/// <seealso cref="Continue"/>
/// <seealso cref="QuitGame"/>
/// </summary>
public class MainMenu : MonoBehaviour {

    public string levelToLoad;
    public string levelSelect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewGame()
    {
        PlayerPrefs.DeleteKey("PlayerLives");
        SceneManager.LoadScene(levelToLoad);
    }

    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
