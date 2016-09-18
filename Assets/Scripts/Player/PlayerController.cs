using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Player movement
    public float moveSpeed;
    public float jumpPower;
    private Rigidbody2D playerRigidbody2D;

    // Ground checking
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;

    // Animation
    private Animator playerAnim;


	// Use this for initialization
	void Start () {

        // Attaches the players Rigidbody2D to the object which this script is attached to
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        // Attaches the players Animator to the object which this script is attached to
        playerAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        // If within a circular area, set isGrounded to true
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Player movement +x and -x
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            playerRigidbody2D.velocity = new Vector3(moveSpeed, playerRigidbody2D.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            playerRigidbody2D.velocity = new Vector3(-moveSpeed, playerRigidbody2D.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else
        {
            playerRigidbody2D.velocity = new Vector3(0f, playerRigidbody2D.velocity.y, 0f);
        }

        // Player jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRigidbody2D.velocity = new Vector3(playerRigidbody2D.velocity.x, jumpPower, 0f);
        }

        // Animation
        playerAnim.SetFloat("Speed", Mathf.Abs(playerRigidbody2D.velocity.x));
        playerAnim.SetBool("Grounded", isGrounded);
    }
}
