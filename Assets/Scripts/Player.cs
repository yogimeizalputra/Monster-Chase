using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;
    private float movementX;
    [SerializeField]
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private string WALK_ANIMATION = "Walk";
    private bool isGrounded;
    private string GROUND_TAG = "Ground";

    private void Awake() {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatedPlayer();
    }
    private void FixedUpdate()
    {
        PlayerJump();
    }

    void PlayerMoveKeyboard() 
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.deltaTime;
    }

    void AnimatedPlayer() 
    {
        if(movementX > 0) 
        {
            // going to right side
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        } else if (movementX < 0) 
        {
            //going to left side
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        } else 
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump() 
    {
        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            isGrounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
        }
    
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(GROUND_TAG)) {
            isGrounded = true;
        }
    }
} // class
