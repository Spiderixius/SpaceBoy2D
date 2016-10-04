using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Player movement
    public float moveSpeed;
    private float activeMoveSpeed;
    public float jumpPower;
    private Rigidbody2D playerRigidbody2D;
    public bool canMove;

    // Ground checking
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;

    // Moving platforms
    private bool onPlatform;
    public float onPlatformMoveSpeed;

    // Animation
    private Animator playerAnim;

    // Audio
    public AudioSource jumpSound;
    public AudioSource hurtSound;

    // Fireball related
    public Transform firePoint;
    public GameObject fireball;
    public float shotDelay;
    private float shotDelayCounter;

    // Knockback related
    public float knockBackForce;
    public float knockBackDuration;
    private float knockBackCounter;

    public float invincibilityLength;
    private float invincibilityCounter;

    // Stompbox related
    public GameObject stompBox;

    // The level manager
    public LevelManager theLevelManager;


    // Use this for initialization
    void Start () {

        // Attaches the players Rigidbody2D to the object which this script is attached to
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        theLevelManager = FindObjectOfType<LevelManager>();

        // Attaches the players Animator to the object which this script is attached to
        playerAnim = GetComponent<Animator>();

        activeMoveSpeed = moveSpeed;

        canMove = true;
	}
	
	// Update is called once per frame
	void Update () {

        // If within a circular area, set isGrounded to true
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (knockBackCounter <= 0 && canMove)
        {
            if (onPlatform)
            {
                activeMoveSpeed = moveSpeed * onPlatformMoveSpeed;
            } else
            {
                activeMoveSpeed = moveSpeed;
            }

            // Player movement +x and -x
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                playerRigidbody2D.velocity = new Vector3(activeMoveSpeed, playerRigidbody2D.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            } else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                playerRigidbody2D.velocity = new Vector3(-activeMoveSpeed, playerRigidbody2D.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            } else
            {
                playerRigidbody2D.velocity = new Vector3(0f, playerRigidbody2D.velocity.y, 0f);
            }

            // Player jump
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerRigidbody2D.velocity = new Vector3(playerRigidbody2D.velocity.x, jumpPower, 0f);
                jumpSound.Play();
            }

            
        }

        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if (transform.localScale.x > 0)
            {
                playerRigidbody2D.velocity = new Vector3(-knockBackForce, knockBackForce, 0f);
            }
            else
            {
                playerRigidbody2D.velocity = new Vector3(knockBackForce, knockBackForce, 0f);
            }
        }

        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }

        if (invincibilityCounter <= 0)
        {
            theLevelManager.invincible = false;
        }

        // Animation
        playerAnim.SetFloat("Speed", Mathf.Abs(playerRigidbody2D.velocity.x));
        playerAnim.SetBool("Grounded", isGrounded);

        // Stompbox deactivation/activation
        if (playerRigidbody2D.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }

        // Shooting fireballs
        // Delay shooting
        shotDelayCounter -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Return) && canMove)
        {
            if (shotDelayCounter <= 0)
            {
                Instantiate(fireball, firePoint.position, firePoint.rotation);
                shotDelayCounter = shotDelay;
            }
        }
        // Consistent shooting
        if (Input.GetKey(KeyCode.Return) && canMove)
        {
            shotDelayCounter -= Time.deltaTime;

            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = shotDelay;
                Instantiate(fireball, firePoint.position, firePoint.rotation);
            }
        }

    }

    public void KnockBack()
    {
        knockBackCounter = knockBackDuration;
        invincibilityCounter = invincibilityLength;
        theLevelManager.invincible = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
            onPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            onPlatform = false;
        }
    }
}
