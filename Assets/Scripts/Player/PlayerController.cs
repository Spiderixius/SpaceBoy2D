using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Variable jumping
    public float timeHeld;
    public float timeForFullJump;
    public float minJumpForce;
    public float maxJumpForce;

    // Player movement
    public float moveSpeed;
    private float activeMoveSpeed;
    public float maxJumpPower;
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

        // If within the groundCheck's circular area, set isGrounded to true if the type is whatIsGround LayerMask. 
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

            //// Player jump
            //if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            //{
            //    playerRigidbody2D.velocity = new Vector3(playerRigidbody2D.velocity.x, maxJumpPower, 0f);
            //    jumpSound.Play();
            //}


            // Jump control
            if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
            {
                timeHeld = 0f;
            }
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                timeHeld += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Jump();
            }


        }

        // Knockback counter, which simply knocks the player in either +x or -x direction.
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

        // Simply counts down the invisibility duration.
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }

        // Sets invisibility back to false. 
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

    /// <summary>
    /// A method to knock back the player upon damage. 
    /// <seealso cref="HurtPlayer()"/>
    /// </summary>
    public void KnockBack()
    {
        knockBackCounter = knockBackDuration;
        invincibilityCounter = invincibilityLength;
        theLevelManager.invincible = true;
    }


    private void Jump()
    {
        float verticalJumpForce = ((maxJumpForce - minJumpForce) * (timeHeld / timeForFullJump)) + minJumpForce;
        if (isGrounded)
        {
            if (verticalJumpForce > maxJumpForce)
            {
                verticalJumpForce = maxJumpForce;
            }
        
            playerRigidbody2D.velocity = new Vector3(playerRigidbody2D.velocity.x, verticalJumpForce, 0f);
            jumpSound.Play();

        }
    }

/// <summary>
/// When the player gameObject has made contact with the other gameobject's collider the player gameObject
/// will become a child of it.
/// This is to ensure that the Player will stay on the moving gameObject and not slide off.
/// </summary>
/// <param name="other">Indicates a seperate gameObject's 2D collider, that is not the player's.</param>
void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
            onPlatform = true;
        }
    }
    /// <summary>
    /// When the Player gameObject has exited stop it from being a child of the other gameObject (MovingPlatform)
    /// </summary>
    /// <seealso cref="Update() -> onPlatformMoveSpeed"/>
    /// <param name="other">Indicates a seperate gameObject's 2D collider, that is not the player's.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            onPlatform = false;
        }
    }
}


