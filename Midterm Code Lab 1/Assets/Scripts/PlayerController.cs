using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerCamera = null;
    public float MouseSens = 3f;
    public float walkSpeed = 6;

    [Range(0f, .5f)] float moveSmoothTime = .3f;
    [Range(0f, .5f)] float mouseSmoothTime = .03f;
    
    public float gravity = -10f;
    public float jumpStrength = 5f;

    //keeps track of camera current x rotation
    private float cameraPitch = 0f;
    //keeps track of downward speed
    private float velocityY = 0f;
    private bool lockCursor = true;

    private CharacterController controller = null;
    
    //stores current direction
    Vector2 currentDir = Vector2.zero;
    //stores current direction velocity
    Vector2 currentDirVelocity = Vector2.zero;
    
    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();

    }

    void UpdateMouseLook()
    {
        //mouse delta stores the input of the x and y axis
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta =
            Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        //inverted y axis so have to do subtraction
        //vertical delta influences camera pitch
        cameraPitch -= currentMouseDelta.y * MouseSens;

        //clamps camera movement so it cant go past 90 degrees either way
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        //setting the camera angles so it rotates around the right angle of camera pitch
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        
        //horiziontal movement
        //rotates in x axis
        transform.Rotate(Vector3.up * currentMouseDelta.x * MouseSens);
        
    }

    void UpdateMovement()
    {
        //input axis can be thought of as a value -1 to 1
        //when there are no keys being pressed it as 0
        //pulled upward to positive 1 when W is pressed
        //pulled downwards to -1 when S is pressed
        //have to multiple this value by a vector that exists in 3D space
        //direction of forward and backwards is dependant on the camera
        //horizontal axis is A and D keys or left and right
        //we want to access the raw value of 1, 0, -1 so it goes instantly to those axis

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        //normalize vector limits it to 1 when going diagonally, will move at the same speed if it was moving on a single axis
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        //define velocity vector
        //set to forward scaled by vertical axis
        //value of velocity y is negative, that's why we use vecotr3.up
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        //call controllers move function
        //pass in velocity * time.deltatime
        controller.Move(velocity * Time.deltaTime);
        
        //smoothDamp allows you to smoothly move between two vectors

        if (controller.isGrounded)
        {
            velocityY = 0f;

            velocityY += gravity * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocityY += Mathf.Sqrt(gravity * -2f * jumpStrength);
            }
            
        }
        else
        {
            velocityY += gravity * Time.deltaTime;
        }

        
    }
    

    
}
