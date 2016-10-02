using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    // Player respawning
    public float waitToRespawn;
    public PlayerController thePlayer;
    public string levelToLoad;

    // Particle effects
    public GameObject deathParticleYellow;

    // Coin related
    public int coinCount;
    public Text coinText;

    // Health related
    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int maxHealth;
    public int healthCount;

    // To make sure that the player is not attempted to be respawned.
    private bool isRespawning;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();

        coinText.text = "Coins: " + coinCount;

        healthCount = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (healthCount <= 0 && !isRespawning)
        {
            Respawn();
            isRespawning = true;
        }
	}

    public void Respawn()
    {
        StartCoroutine("RespawnCo");
    }

    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);

        Instantiate(deathParticleYellow, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(waitToRespawn);

        healthCount = maxHealth;
        isRespawning = false;
        UpdateHeartSprites();

        SceneManager.LoadScene(levelToLoad);
        //thePlayer.gameObject.SetActive(true);
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinText.text = "Coins: " + coinCount;
    }

    public void HurtPlayer(int hurtAmount)
    {
        healthCount -= hurtAmount;
        UpdateHeartSprites();

        thePlayer.hurtSound.Play();
    }

    public void UpdateHeartSprites()
    {
        switch (healthCount)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
        }
    }
}
