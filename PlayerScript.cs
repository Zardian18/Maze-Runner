using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody myBody;
    Animator anim;
    bool isMoving;
    [SerializeField]
    float speed = 0.5f;
    [SerializeField]
    float rotationSpeed = 4f;
    [SerializeField]
    float jumpForce = 3f;
    bool canJump;
    float moveHorizontal, moveVertical;
    float rotY = 0f;

    [SerializeField]
    GameplayController gc;

    [SerializeField]
    float xSpeed = 2f, ySpeed = 5f, jumpSpeed = 10f, gravity = 10f;
    float yVelocity = 0f;
    Vector3 moveVelocity = Vector3.zero;

    public Transform groundCheck;
    public LayerMask groundLayer;

    CharacterController controller;

    [SerializeField]
    GameObject damagePoint;

    PlayerAudio audio;
    // Start is called before the first frame update

    void Awake()
    {
        //myBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audio = GetComponent<PlayerAudio>();
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        rotY = transform.localEulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        KeyboardMovement();
        AnimatePlayer();
        Attack();
        //IsOnGround();
        //Jump();
        // RotateAndMove();
        Movement();
    }
    private void FixedUpdate()
    {
        //RotateAndMove();
    }
    void KeyboardMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1;
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            moveHorizontal = 0;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1;
        }
        if(Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.S))
        {
            moveVertical = 0;
        }


        
    }
    void RotateAndMove()
    {

       /* if (moveVertical != 0)
        {
            myBody.MovePosition(transform.position + transform.right * (moveHorizontal * speed) + transform.forward * (moveVertical * speed));

        }
        if (moveHorizontal != 0)
        {
            myBody.MovePosition(transform.position + transform.right * (moveHorizontal * speed)+ transform.forward*(moveVertical*speed));
        }*/


        /*Vector3 pos = new Vector3(moveHorizontal, 0f, moveVertical);
        pos.Normalize();
        myBody.MovePosition(transform.position + pos*speed);*/
        /*rotY += moveHorizontal * rotationSpeed;
        myBody.rotation = Quaternion.Euler(0f, rotY, 0f);*/
    }

    void Movement()
    {
        /*moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");*/
        moveVelocity = new Vector3(moveHorizontal * xSpeed, 0, moveVertical * ySpeed);

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
                anim.SetTrigger(Tags.JUMP_TRIGGER);
                audio.PlayjumpClip();

            }
            Debug.Log("Grounded");
        }
        else
        {
            yVelocity -= gravity;
            Debug.Log("Not grounded");

        }
        moveVelocity.y = yVelocity;
        moveVelocity = transform.transform.TransformDirection(moveVelocity);
        controller.Move(moveVelocity * Time.deltaTime);
        Debug.Log(moveVelocity);
    }
    void AnimatePlayer()
    {
        if (moveVertical != 0 || moveHorizontal!=0)
        {
            if (!isMoving)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.RUN_ANIM))
                {
                    isMoving = true;
                    anim.SetTrigger(Tags.RUN_TRIGGER);
                }
            }
            audio.PlayRunClip();
        }
        else
        {
            //Debug.Log("stop");
            if (isMoving)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.RUN_ANIM))
                {
                    anim.SetTrigger(Tags.STOP_TRIGGER);
                    isMoving = false;
                    anim.SetTrigger(Tags.STOP_TRIGGER);
                }
            }
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F))
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.ATTACK_ANIM)|| !anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.Run_ATTACK_ANIM))
            {
                anim.SetTrigger(Tags.ATTACK_ANIM);
                audio.PlayAttackClip();
            }
        }
    }

    void IsOnGround()
    {
        canJump = Physics.Raycast(groundCheck.position, Vector3.down, 0.1f, groundLayer);
    }
    void Jump()
    {
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                canJump = false;
                myBody.MovePosition(transform.position + transform.up * (jumpForce*speed));
                anim.SetTrigger(Tags.JUMP_TRIGGER);
                audio.PlayjumpClip();
            }
        }
    }
    
    void ActivateDamagePoint()
    {
        damagePoint.SetActive(true);
    }

    void DeactivateDamagePoint()
    {
        damagePoint.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            
            Debug.Log("Collided with wall");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Victory")
        {
            gc.VicotryDisplay();
            Debug.Log("you win");
        }
    }
}
