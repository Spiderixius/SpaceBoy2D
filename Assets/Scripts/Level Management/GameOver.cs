using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

    public void Restart()
    {
        PlayerPrefs.DeleteKey("PlayerLives");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
