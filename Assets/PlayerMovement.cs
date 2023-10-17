using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
          private float horizontal;
          private float speed = 8f;
          private float jumpingPower = 16f;
          private bool isFacingRight = true;

          private bool isWallSliding;
          private float wallSlidingSpeed = 2f;

          private bool isWallJumping;
          private float wallJumpingDirection;
          private float wallJumpingTime = 0.2f;
          private float wallJumpingCounter;
          private float wallJumpingDuration = 0.4f;
          private Vector2 wallJumpingPower = new Vector2(8f, 16f);

          [SerializeField] private Rigidbody2D rb;
          [SerializeField] private Transform groundCheck;
          [SerializeField] private LayerMask groundLayer;
          [SerializeField] private Transform wallCheck;
          [SerializeField] private LayerMask wallLayer;

          private void Update()
          {
              horizontal = Input.GetAxisRaw("Horizontal");

              if (Input.GetButtonDown("Jump") && (IsGrounded() || IsWalled()))
              {
                  if (IsGrounded())
                  {
                      rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                  }
                  else if (IsWalled() && !IsGrounded() && horizontal != 0f)
                  {
                      isWallJumping = true;
                      rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                      Invoke(nameof(StopWallJumping), wallJumpingDuration);
                  }
              }

              if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
              {
                  rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
              }

              WallSlide();
              if (!isWallJumping)
              {
                  Flip();
              }
            Debug.Log("Is Wall Sliding: " + isWallSliding);
            Debug.Log("Is Touching Wall: " + IsWalled());
            Debug.Log("Is Grounded: " + IsGrounded());
            Debug.Log("Is Wall Jumping: " + isWallJumping);
        }

          private void FixedUpdate()
          {
              if (!isWallJumping)
              {
                  rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
              }
          }

          private bool IsGrounded()
          {
              return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
          }

          private bool IsWalled()
          {
              return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
          }
    
          private void WallSlide()
          {
              if (IsWalled() && !IsGrounded() && horizontal != 0f)
              {
                  isWallSliding = true;
                  rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
              }
              else
              {
                  isWallSliding = false;
              }
          }
    
          private void WallJump()
          {
              if (isWallSliding)
              {
                  wallJumpingDirection = -transform.localScale.x;
                  wallJumpingCounter = wallJumpingTime;
                  CancelInvoke(nameof(StopWallJumping));
              }
              else
              {
                  wallJumpingCounter -= Time.deltaTime;
              }
          }

          private void StopWallJumping()
          {
              isWallJumping = false;
          }

          private void Flip()
          {
              if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
              {
                  isFacingRight = !isFacingRight;
                  Vector3 localScale = transform.localScale;
                  localScale.x *= -1f;
                  transform.localScale = localScale;
              }
          }
    /*
   public float walkSpeed;
   private float moveInput;
   public float jumpSpeed;
   private bool isGrounded;
   private Rigidbody2D rb;
   public LayerMask groundMask;
   private bool isTouchingLeft;
   private bool isTouchingRight;
   private bool wallJumping;
   private float touchingLeftOrRight;

   private void Start()
   {
       rb = gameObject.GetComponent<Rigidbody2D>();
   }
   void Update()
   {
       moveInput = Input.GetAxisRaw("Horizontal");

       if ((!isTouchingLeft && !isTouchingRight) || isGrounded)
       {

           rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
       }

       if (Input.GetKeyDown("space") && isGrounded)
       {
           rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
       }

       isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f),
           new Vector2(0.9f, 0.2f), 0f, groundMask);

       isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y),
           new Vector2(0.2f, 0.9f), 0f, groundMask);

       isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y),
           new Vector2(0.2f, 0.9f), 0f, groundMask);

       if(isTouchingLeft)
       {
           touchingLeftOrRight = 1;
       }
       else if (isTouchingRight)
       {
           touchingLeftOrRight = -1;
       }
       if (Input.GetKeyDown("space")&&(isTouchingRight || isTouchingLeft) && !isGrounded)
       {
           wallJumping = true;
           Invoke(nameof(SetJumpingToFalse));
       }
       if(wallJumping)
       {
           rb.velocity = new Vector2(walkSpeed * touchingLeftOrRight, jumpSpeed);
       }
   }

   private void Invoke(string v)
   {
       throw new NotImplementedException();
   }

   void OnDrawGizmosSelected()
   {
       Gizmos.color = Color.green;
       Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f),new Vector2(0.9f, 0.2f));

       Gizmos.color = Color.blue;
       Gizmos.DrawCube(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y),new Vector2(0.2f, 0.9f));
       Gizmos.DrawCube(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y), new Vector2(0.2f, 0.9f));
   }
   void SetJumpingToFalse()
   {
       wallJumping = false;
   }
*/


    /*
      private Rigidbody2D rb;



        int angka = 16;
         float angkaDesimal = 4.54f;
         string text = "lala";
         bool boolean = false;


   // Start is called before the first frame update
   private void Start()
     {
         rb = GetComponent<Rigidbody2D>();
     }




     // Update is called once per frame
     private void Update()
     {
         float dirx = Input.GetAxisRaw("Horizontal");
         rb.velocity = new Vector2(dirx * 7f, rb.velocity.y);

        // Debug.Log("Hello,world!");
        if (Input.GetButtonDown("Jump"))
         {
           rb.velocity = new Vector3(rb.velocity.x, 14f);
         }
     } 
 */
}
