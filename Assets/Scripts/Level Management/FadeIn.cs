using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Code to fade the screen from black to fully transparent. 
/// </summary>
public class FadeIn : MonoBehaviour {

    public float fadeTime;

    private Image blackScreen;

	// Use this for initialization
	void Start () {
        blackScreen = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        blackScreen.CrossFadeAlpha(0f, fadeTime, false);

        if (blackScreen.color.a == 0)
        {
            Destroy(gameObject);
        }
	}
}
