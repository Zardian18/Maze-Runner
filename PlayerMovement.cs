using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Transform groundCheck;
    Rigidbody myBody;
    Animator anim;
    [SerializeField]
    float speed;
    [SerializeField]
    float runningSpeed;
    float orignalSpeed;
    float rotY;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float jumpSpeed;
    bool canJump;
    bool isGrounded = true;
    Vector3 velZ;    //velocity Z
    Vector3 velX;
    bool isMoving;
    bool isRunning;
    bool pressingShift;
    bool canClick = true;
    [SerializeField]
    float refreshTime;
    int moveX, moveZ;
    [SerializeField]
    Transform raycastCheck;
    public bool canAttack=true;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        orignalSpeed = speed;
        rotY = transform.localRotation.eulerAngles.y;
    }

    void Update()
    {
        Movement();
        if (canAttack)
        {
            RunningAnim();
            BlockAnim();
            if (canClick)
            {
                AttackSingleAnim();
                HeavyAttackAnim();

            }
            else if (!canClick)
            {
                StartCoroutine(ToogleClick());
            }
            if (isGrounded && canJump)
            {
                Jumping();
            }
        }
        
        WalkAnim();
       //noVelocity();
        
        
        CheckShift();
        
        CheckCanJump();
    }
    private void FixedUpdate()
    {
        
        
    }
    void noVelocity()
    {
        if(!(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            myBody.velocity = Vector3.zero;
        }
    }
    void Movement()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            velZ = new Vector3(myBody.velocity.x, myBody.velocity.y, speed);
            isMoving = true;
            moveZ = 1;
            
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            velZ=new Vector3(myBody.velocity.x,myBody.velocity.y,0f);
            isMoving = false;
            moveZ = 0;
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            velZ =new Vector3(myBody.velocity.x ,myBody.velocity.y, -speed);
            isMoving = true;
            moveZ = -1;
            
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            velZ=new Vector3(myBody.velocity.x, myBody.velocity.y, 0f);
            isMoving = false;
            moveZ = 0;
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            velX = new Vector3(speed, myBody.velocity.y, myBody.velocity.z);
            isMoving = true;
            moveX = 1;
            
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            velX = new Vector3(0f, myBody.velocity.y, myBody.velocity.z);
            isMoving = false;
            moveX = 0;
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            velX = new Vector3(-speed, myBody.velocity.y, myBody.velocity.z);
            isMoving = true;
            moveX = -1;
            
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            velX = new Vector3(0f, myBody.velocity.y, myBody.velocity.z);
            isMoving = false;
            moveX = 0;
            
        }
        Run();
       //rotY += moveX * rotationSpeed;   // -----> if not using mouse
       //myBody.rotation = Quaternion.Euler(0f, rotY, 0f);
        transform.Translate(Vector3.Normalize((velZ + velX))*Time.deltaTime*speed);
    }
    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) &&isMoving)
        {
            speed = runningSpeed;
            isRunning = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = orignalSpeed;
            isRunning = false;
        }
    }
    void WalkAnim()
    {
        if (isMoving)
        {
            if (!anim.GetBool("Walk"))
            {
                anim.SetBool("Walk", true);
            }
        }
        else if (!isMoving)
        {
            anim.SetBool("Walk", false);
        }
    }
    void RunningAnim()
    {
        if (isRunning)
        {
            if (!anim.GetBool("Run"))
            {
                anim.SetBool("Run", true);
            }
        }
        else if (!isRunning)
        {
            anim.SetBool("Run", false);
        }
    }
    void AttackSingleAnim()
    {
        if (Input.GetMouseButtonDown(0) && !pressingShift)
        {
            
            if (!anim.GetBool("AttackSingle"))
            {
                anim.SetBool("AttackSingle", true);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("AttackSingle", false);
        }
        else if (Input.GetMouseButton(0))
        {
            //spam
            anim.SetBool("AttackSingle", false);
        }
    } 
    void HeavyAttackAnim()
    {
        if (Input.GetMouseButtonDown(0) && pressingShift)
        {
            
            if (!anim.GetBool("AttackPoke"))
            {
                anim.SetBool("AttackPoke", true);
                anim.SetBool("Walk", false);
                
               // StartCoroutine(PlayerShift());
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("AttackPoke", false);
            
        }
        else if (Input.GetMouseButton(0))
        {
            //spam
            anim.SetBool("AttackPoke", false);
        }
    }
    void BlockAnim()
    {
        if (Input.GetMouseButtonDown(1))
        {

            if (!anim.GetBool("Block"))
            {
                anim.SetBool("Block", true);
                //anim.SetBool("Walk", false);

                // StartCoroutine(PlayerShift());
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("Block", false);

        }
        else if (Input.GetMouseButton(1))
        {
            //spam
            anim.SetBool("Block", true);
        }
    }
    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canJump = false;
            if (!anim.GetBool("Jump"))
            {
                myBody.velocity += new Vector3(0f, jumpSpeed, 0f);
                anim.SetBool("Jump", true);
            }
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Jump", false);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("Jump", false);
        }
    }
    IEnumerator DelayedJump(float time)
    {
        yield return new WaitForSeconds(time);
        if (!anim.GetBool("Jump"))
        {
            myBody.velocity += new Vector3(0f, jumpSpeed, 0f);
            anim.SetBool("Jump", true);
        }
    }

    void CheckShift()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            pressingShift = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            pressingShift = false;
        }
    }
    IEnumerator PlayerShift()
    {
        yield return new WaitForSeconds(3f);
        transform.position += new Vector3(0f, 0f, 2f);
    }

    IEnumerator ToogleClick()
    {
        canClick = false;
        yield return new WaitForSeconds(refreshTime);
        canClick = true;
    }
    void CheckCanJump()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(raycastCheck.transform.position,Vector3.down, out hitInfo, 0.5f, groundLayer))
        {
            canJump = true;
            isGrounded = true;
        }
        else
        {
            canJump = false;
            isGrounded = false;
        }
    }
}
