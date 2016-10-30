using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// The level manager controls several aspects of the level, from coins to health to death handling.
/// </summary>
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

    /// <summary>
    /// The respawn method which starts the respawn coroutine
    /// Also PlayerPrefs are reset.
    /// <see cref="RespawnCo"/>
    /// </summary>
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

    /// <summary>
    /// A cooroutine used to control the amount the player has to wait before being respawned.
    /// This method is used to let the player catch a breath before being cast into the game again.
    /// </summary>
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

    /// <summary>
    /// A method to add coins.
    /// </summary>
    /// <param name="coinsToAdd">Coins amount to add.</param>
    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinBonusLifeCount += coinsToAdd;
        coinSound.Play();
        coinText.text = "Coins: " + coinCount;
    }

    /// <summary>
    /// A method to hurt the player and the amount.
    /// </summary>
    /// <param name="hurtAmount">Amount of damage to inflict. </param>
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

    /// <summary>
    /// Method to add health, and the amount of health to be added.
    /// </summary>
    /// <param name="healthToAdd">The amount health to be added.</param>
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

    /// <summary>
    /// Method to add lives, and the amount of lives to be added.
    /// </summary>
    /// <param name="livesToAdd">The amount of lives to be added.</param>
    public void AddLives(int livesToAdd)
    {
        currentLives += livesToAdd;
        livesText.text = "Lives x " + currentLives;
    }

    /// <summary>
    /// A method to update the heart sprites once the health amount has been updated. 
    /// </summary>
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
