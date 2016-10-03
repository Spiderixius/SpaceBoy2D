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
    public AudioSource coinSound;
    public int bonusLifeThreshold;
    private int coinBonusLifeCount;

    // Health related
    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int maxHealth;
    public int healthCount;

    // Knockback related
    public bool invincible;

    // Lives related
    public int startingLives;
    public int currentLives;
    public Text livesText;
    public AudioSource livesSound;

    // GameOver Screen
    public GameObject gameOverScreen;
    public AudioSource gameOverMusic;

    // Music
    public AudioSource levelMusic;


    // To make sure that the player is not attempted to be respawned.
    private bool isRespawning;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();

        coinText.text = "Coins: " + coinCount;

        healthCount = maxHealth;

        if (PlayerPrefs.HasKey("PlayerLives"))
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives");
        }
        else
        {
            currentLives = startingLives;
        }
        
        livesText.text = "Lives x " + currentLives;

	}
	
	// Update is called once per frame
	void Update () {
        if (healthCount <= 0 && !isRespawning)
        {
            Respawn();
            isRespawning = true;
        }

        if (coinBonusLifeCount >= bonusLifeThreshold)
        {
            currentLives += 1;
            livesText.text = "Lives x " + currentLives;
            coinBonusLifeCount -= bonusLifeThreshold;
        }
	}

    public void Respawn()
    {
        currentLives -= 1;
        livesText.text = "Lives x " + currentLives;

        if (currentLives > 0)
        {
            StartCoroutine("RespawnCo");
        }
        else
        {
            thePlayer.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
            levelMusic.Stop();
            gameOverMusic.Play();
            PlayerPrefs.DeleteKey("PlayerLives");
        }
    }

    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);

        Instantiate(deathParticleYellow, thePlayer.transform.position, thePlayer.transform.rotation);

        PlayerPrefs.SetInt("PlayerLives", currentLives);
        yield return new WaitForSeconds(waitToRespawn); 
        healthCount = maxHealth;
        isRespawning = false;
        UpdateHeartSprites();
        coinBonusLifeCount = 0;
        SceneManager.LoadScene(levelToLoad);
        //thePlayer.gameObject.SetActive(true);
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinBonusLifeCount += coinsToAdd;
        coinSound.Play();
        coinText.text = "Coins: " + coinCount;
    }

    public void HurtPlayer(int hurtAmount)
    {
        if (!invincible)
        {
            healthCount -= hurtAmount;
            UpdateHeartSprites();
            thePlayer.KnockBack();
            thePlayer.hurtSound.Play();
        }
        
    }

    public void AddHealth(int healthToAdd)
    {
        healthCount += healthToAdd;

        // To avoid having more health than it is possible.
        if (healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }

        UpdateHeartSprites();
    }

    public void AddLives(int livesToAdd)
    {
        currentLives += livesToAdd;
        livesText.text = "Lives x " + currentLives;
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
