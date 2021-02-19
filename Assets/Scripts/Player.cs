using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;
    [SerializeField, Range(0.1f, 20f)]
    float moveSpeed = 2f;
    [SerializeField, Range(0.1f, 15f)]
    float jumpForce = 5;
    Rigidbody2D rb2D;
    [SerializeField]
    ContactFilter2D groundFilter;
    PlayerControls playerControls;
    
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();   
    }
    void Start()
    {
        playerControls.Gameplay.Jump.performed += ctx => Jump();
        playerControls.Gameplay.Power.performed += ctx => Power();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable() 
    {
        playerControls.Disable();
    }

    void Update()
    {
        transform.Translate(Vector2.right * axis.x * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if(IsGrounded)
        {
            animator.SetTrigger("jump");
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Power()
    {
        if(IsGrounded)
        {
            animator.SetTrigger("power");
        }
    }

    void LateUpdate() 
    {
        sprite.flipX = flipSprite;
        animator.SetFloat("moveX", Mathf.Abs(axis.x));
    }

    void FixedUpdate() 
    {
        animator.SetBool("isGrounded", IsGrounded);
    }

    Vector2 axis => playerControls.Gameplay.Movement.ReadValue<Vector2>();
    bool flipSprite => axis.x > 0 ? false : axis.x < 0 ? true : sprite.flipX;
    bool IsGrounded => rb2D.IsTouching(groundFilter);
}
