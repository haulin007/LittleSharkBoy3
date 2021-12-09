using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    private MainControl mainControl;
    new private Transform transform;
    private Rigidbody2D rigitBody;
    private SpriteRenderer sprite; 
    public bool buttonLeft { get; set; }
    public bool buttonRight { get; set; }
    public bool buttonJump { get; set; }

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Animator animator;
    private int animationState = 0;

    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    const float maxMoveSpeed = 40.0f;
    [SerializeField]
    private float jumpForce = 10.0f;
    [SerializeField]
    private float moveForce = 5.0f;
    [SerializeField]
    private float maxVelocity = 2.0f;
    [SerializeField]
    private float normalGravity = 3.0f;
    [SerializeField]
    private bool canMove = true;
    [SerializeField]
    private bool isCharacterPowerfull = false;

    [SerializeField]
    private float superPowerJumpIncrement = 10.0f;
    [SerializeField]
    private float superPowerMoveSpeedIncrement = 10.0f;
    [SerializeField]
    private float jumpForceIncriment = 10.0f;
    [SerializeField]
    private float maxVelocityIncriment = 3.0f;
    [SerializeField]
    private float superPowerDecay  = 8.0f;

    [SerializeField]
    private int health = 69;
    private int maxHealth = 69;

    private bool isGrounded = true;
    void Start()
    {
        buttonRight = false;
        buttonLeft = false;
        buttonJump = false;
        mainControl = FindObjectOfType<MainControl>();
        healthText.text = health.ToString();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        transform = gameObject.GetComponent<Transform>();
        rigitBody = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (canMove)
        {
            groundChek();
            if(isGrounded) 
                animator.SetInteger("state", 0);
            else
                animator.SetInteger("state", 1);

            if (buttonLeft || Input.GetKey("a")) moveLeft();
            if (buttonRight || Input.GetKey("d")) moveRight();
            if ((buttonJump == true || Input.GetButtonDown("Jump")) && isGrounded)
            {
                buttonJump = false;
                jump(); 
            }
        } 
    }
    public void moveLeft()
    {
        if (Mathf.Abs(rigitBody.velocity.x) < maxVelocity) rigitBody.AddForce(Vector2.left * moveForce, ForceMode2D.Impulse);
        //transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        sprite.flipX = true;
        if (isGrounded)
        {
            animator.SetInteger("state", 2);
        }
    }
    public void moveRight()
    {
        if (Mathf.Abs(rigitBody.velocity.x) < maxVelocity) rigitBody.AddForce(Vector2.right * moveForce, ForceMode2D.Impulse);
        //transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        sprite.flipX = false;
        if (isGrounded)
        {
            animator.SetInteger("state", 2);
        }
    }
    public void jump()
    {
        rigitBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    public void setMoveSpeed(float newSpeed)
    {
        if (newSpeed > 0 && newSpeed < maxMoveSpeed)
        {
            moveSpeed = newSpeed;
        }
        else
            Debug.LogWarning("Max move spear are upgrating!");
    }
    private void groundChek()
    {
        Collider2D[] colliders =  Physics2D.OverlapCircleAll(transform.position + new Vector3(0,-0.7f,0), 0.4f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Ground")
            {
                isGrounded = true;
                return;
            }
            else
                isGrounded = false;
        }
    }
    public void getSuperPower()
    {
        StartCoroutine("superPowerDelay");
    }
    IEnumerator superPowerDelay()
    {
        maxVelocity += maxVelocityIncriment;
        moveForce += jumpForceIncriment;

        jumpForce += superPowerJumpIncrement;
        moveSpeed += superPowerMoveSpeedIncrement;
        yield return new WaitForSecondsRealtime(5);
        jumpForce -= superPowerJumpIncrement;
        moveSpeed -= superPowerMoveSpeedIncrement;

        moveForce -= jumpForceIncriment;
        maxVelocity -= maxVelocityIncriment;
    }
    public bool getHealt(int incHealth)
    {
        if (health < maxHealth)
            health += incHealth;
        else
            return false;
        if (health > maxHealth) health = maxHealth;

        healthText.text = health.ToString();
        return true;
    }
    public void setMove(bool newMove)
    {
        canMove = newMove;
    }
    public void getDamage(int damagePower)
    {
        if (damagePower > health)
        {
            health = 0;
            Death();
        }
        else
        {
            health -= damagePower;
        }
        healthText.text = health.ToString();
    }
    public void setNoGravity()
    {
        rigitBody.gravityScale = 0.0f;
    }
    public void setNormalGravity()
    {
        rigitBody.gravityScale = normalGravity; ;
    }
    public void getPowerfull()
    {
        isCharacterPowerfull = true;
    }
    public bool isPowerfull()
    {
        return isCharacterPowerfull;
    }
    private void Death()
    {
        Debug.Log("Death");
        mainControl.characterIsDeath();
    }
}
