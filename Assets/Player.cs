using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private CharacterController characterController;
    [SerializeField] private GameObject camPivot;

    private Vector2 moveVector = new Vector2(0, 0);
    private float yVel = 0f;

    public float moveSpeed = 5f;
    public float rotateSpeed = 0.1f;
    [Range(0f, 0.1f)]
    public float jumpHeight = 0.1f;
    public float accelSpeed = 1f;
    public float deccelSpeed = 5f;
    [Range(-1f, 0f)]
    public float gravity = -1f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        
    }

    // Update is called once per frame
    void Update()
    {
        Lerping();

        // Update the animator with the movement speed
        animator.SetFloat("velocityX", moveVector.x);
        animator.SetFloat("velocityY", moveVector.y);

        // Handle movement
        //Vector3 move3 = new Vector3(moveVector.x, 0f, moveVector.y) * moveSpeed * Time.deltaTime;
        Vector3 move = (transform.forward * moveVector.y + transform.right * moveVector.x) * moveSpeed * Time.deltaTime;
        characterController.Move(move);

        yVel += gravity * Time.deltaTime;
        if (characterController.isGrounded && yVel < 0)
        {
            yVel = 0f;
        }

        camPivot.transform.localEulerAngles = new Vector3(camPivot.transform.localEulerAngles.x - lookInput.y, camPivot.transform.localEulerAngles.y, camPivot.transform.localEulerAngles.z);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + lookInput.x, transform.localEulerAngles.z);

        characterController.Move(new Vector3(0, yVel, 0));

    }

    private void Lerping()
    {
        if (moveInput.x != 0)
        {
            moveVector.x += moveInput.x * accelSpeed * Time.deltaTime;

            moveVector.x = Mathf.Clamp(moveVector.x, -1, 1);
        }
        else
        {
            if (moveVector.x > 0)
            {
                moveVector.x -= deccelSpeed * Time.deltaTime;
                if (moveVector.x < 0)
                {
                    moveVector.x = 0;
                }
            }
            else if (moveVector.x < 0)
            {
                moveVector.x += deccelSpeed * Time.deltaTime;
                if (moveVector.x > 0)
                {
                    moveVector.x = 0;
                }
            }
        }
        if (moveInput.y != 0)
        {
            moveVector.y += moveInput.y * accelSpeed * Time.deltaTime;

            moveVector.y = Mathf.Clamp(moveVector.y, -1, 1);
        }
        else
        {
            if (moveVector.y > 0)
            {
                moveVector.y -= deccelSpeed * Time.deltaTime;
                if (moveVector.y < 0)
                {
                    moveVector.y = 0;
                }
            }
            else if (moveVector.y < 0)
            {
                moveVector.y += deccelSpeed * Time.deltaTime;
                if (moveVector.y > 0)
                {
                    moveVector.y = 0;
                }
            }
        }
    }

    // Method to handle the Clap action started
    public void OnClap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetBool("clapping", true);
        }
        else if (context.canceled)
        {
            animator.SetBool("clapping", false);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        lookInput *= rotateSpeed;
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger("jump");
            yVel = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            moveSpeed *= 2;
            animator.SetFloat("speedMult", 2f);
        }
        else if (context.canceled)
        {
            moveSpeed /= 2;
            animator.SetFloat("speedMult", 1f);
        }
    }


}
