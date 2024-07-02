using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed = 1.0f;
    public float forwardSpeed = 0.0f;

    [Header("Player Animator")]
    public CharacterController characterController;
    public Animator anim;

    [Header("Player Rotation")]
    public float turnCalcRate = 0.1f;
    float turnCalcVelocity;

    [Header("Player Script Camera")]
    public Transform playerCam;

    [Header("Player Jumping and Velocity")]
    public float jumpRange = 1.0f;
    Vector3 velocity;
    public Transform surfaceCheck;
    public bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;
    public float gravity = -9.81f;

    private PlayerControls playerControl;

    


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerControl = new PlayerControls();
    }

    void PlayerMove()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;
        //if(direction.magnitude >= 0.1f)
//        Debug.Log("Direction magnitude : " + direction * movementSpeed * Time.deltaTime);
        
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalcVelocity, turnCalcRate);
        //transform.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
        //transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

        Vector3 moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
        
        transform.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);

        //characterController.Move(direction * movementSpeed * Time.deltaTime);
        
        characterController.Move(moveDir.normalized * forwardSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && onSurface)
        {
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
            anim.SetBool("isJumping", true);
        }
        else if(Input.GetButtonUp("Jump") && !onSurface)
        {
            anim.SetBool("isJumping", false);
        }
    }

    public void OnJump(){
        Debug.Log("NEW INPUT SYSTEM JUMP");
    }

    void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if(onSurface && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);



//        Debug.Log( "Vertical" + Input.GetAxis("Vertical"));

        forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;

        //if(Input.GetMouseButtonDown(0) || 
        if(Input.GetAxis("Aim") > 0)
        {
            anim.SetBool("isAimming", true);
        }
        // else if(Input.GetMouseButtonUp(0))
        else 
            anim.SetBool("isAimming", false);

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = 5;
            anim.SetBool("isRunning", true);
            anim.SetBool("isRunning", true);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = 1;
            anim.SetBool("isRunning", false);
        }

        if(forwardSpeed > 0 && forwardSpeed <= 1.0f)
        {
            anim.SetBool("isWalking", true);
            //anim.SetBool("isRunning", true);
        }
        else if(forwardSpeed > 1)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if(anim.GetBool("isAimming"))
        {
            // if(Input.GetMouseButtonDown(1) || (Input.GetAxis("Fire") > 0))
            if(Input.GetAxis("Fire") > 0)
                anim.SetBool("isShooting", true);
            // else if(Input.GetMouseButtonUp(1))
            else
                anim.SetBool("isShooting", false);

        }


        PlayerMove();
        Jump();
    }

}
