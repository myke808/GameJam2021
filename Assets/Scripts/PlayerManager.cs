using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] public CharacterSelected playerSelected;
    [SerializeField] List<Character> CharacterList = new List<Character>();
    [SerializeField] CapsuleCollider PlayerCollider;
    [SerializeField] float DefaultColliderHeight;
    [SerializeField] Vector3 DefaultColliderY;

    Character TempChar;
    [Header("Player Stats")]
    [SerializeField] public string PlayerName;
    [SerializeField] float runSpeed;
    [SerializeField] int Defense;
    [SerializeField] int Attack;
    [SerializeField] int Capacity;



    [SerializeField] float fallingThreshold = -10f;
    [SerializeField] float jumpHeight;
    [SerializeField] float dashSpeed;
    [SerializeField] int MaxJumpCount;
    [SerializeField] int CurrentJumpCount;


    [SerializeField] public int moveMode = 1;
    [SerializeField] public bool CanRun = false;
    Vector2 moveInput;
    Rigidbody myRigidbody;
    Animator myAnimator;
    CapsuleCollider myCapsuleCollider;
    [SerializeField] bool onGround;
    float CurrentMoveVelocity;

    [Header("Dash Data")]
    float DashPeriod = 1.5f;
    float SlidePeriod = 1f;
    float CurrDashPeriod;
    float CurrSlidePeriod;
    bool IsSliding = false;
    bool IsDashing = false;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponentInChildren<Animator>();
        myCapsuleCollider = GetComponentInChildren<CapsuleCollider>();

        CurrentJumpCount = MaxJumpCount;
        DefaultColliderHeight = PlayerCollider.height;
        DefaultColliderY = PlayerCollider.center;
        //Debug.Log(moveInput);
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        isFalling();

        if(IsDashing)
        {
            CurrDashPeriod -= Time.deltaTime;
            if(CurrDashPeriod <= 0)
            {
                IsDashing = false;
            }
        }
        if(IsSliding)
        {
            CurrSlidePeriod -= Time.deltaTime;
            if(CurrSlidePeriod <= 0)
            {
                IsSliding = false;
                dashSpeed = 0;
                myAnimator.SetBool("isSliding", IsSliding);
                PlayerCollider.height = DefaultColliderHeight;
                PlayerCollider.center = DefaultColliderY;
            }
        }
    }

    public void RefreshCharacter()
    {
        switch (playerSelected)
        {
            case CharacterSelected.Clutch:
                TempChar = CharacterList[0];
                break;
            case CharacterSelected.Beat:
                TempChar = CharacterList[1];
                break;
        }
        PlayerName = TempChar.PlayerName;
        runSpeed = TempChar.PlayerSpeed;
        Defense = TempChar.PlayerDef;
        Attack = TempChar.PlayerAtk;
        jumpHeight = TempChar.PlayerJumpHeight;
        MaxJumpCount = TempChar.PlayerJumpCount;
        name = TempChar.PlayerName;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Trigger:" + collision.gameObject.tag);
        if (collision.gameObject.tag == "floor")
        {
            onGround = true;
            CurrentJumpCount = MaxJumpCount;
            myAnimator.SetBool("isJumping", false);
        }

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("ML" + moveInput);
    }
    void OnJump(InputValue value)
    {
        if (CanRun)
        {
            if (value.isPressed)
            {
                if (onGround)
                {
                    myRigidbody.velocity = Vector3.zero;//Resets all velocity so that the second jump does is not absorbed by gravity
                    myRigidbody.velocity += new Vector3(0f, jumpHeight, 0f);
                    if(CurrentJumpCount != 1)
                    {
                        CurrentJumpCount--;
                    }
                    else
                    {
                        onGround = false; 
                    }
                    myAnimator.SetBool("isJumping", true);
                }
            }
        }
    }
    void Run()
    {
        if(CanRun)
        {
            if (moveMode == 1)//Manual Run
            {
                CurrentMoveVelocity = moveInput.x * runSpeed + dashSpeed;
            }

            else if (moveMode == 2)//Auto Run
            {
                CurrentMoveVelocity = 1;
            }
            Vector2 playerVelocity = new Vector2(CurrentMoveVelocity * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }
    void OnSlide(InputValue value)
    {
        Debug.Log("Slide pressed");
        PlayerCollider.height = 0;
        PlayerCollider.center = Vector3.zero;
        CurrSlidePeriod = SlidePeriod;
        dashSpeed = 10;
        IsSliding = true;
        myAnimator.SetBool("isSliding", IsSliding);
    }

    void Dash()
    {

    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        
    }
    void isFalling()
    {
        if(myRigidbody.velocity.y < fallingThreshold)
        {
            Debug.Log("is Falling");
        }
    }
}