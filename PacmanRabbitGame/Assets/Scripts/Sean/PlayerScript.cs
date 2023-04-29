using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //Movement
    private Animator anim;
    public CharacterController controller;
    private Vector3 playerVelocity;
    public float playerSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 1f;
    private float gravity = -9.81f;

    //Camera Movement
    private float mouseSensitivity = 3f;
    private Transform playerCamera;
    private float xRotationCamera = 0f;

    //Score 
    private ScoreScript score;

    //Sound
    private AudioSource source;
    public AudioClip jump;
    public AudioClip eatCarrot;

    //New input stuff

    //[SerializeField]
    //private InputActionReference INP_movement,INP_look,INP_jump,INP_teleport;
    private Gamepad gamepad = Gamepad.current;
    private Keyboard keyboard = Keyboard.current;
    private Mouse mouse = Mouse.current;
    private InputActionAsset inputAsset;
    private InputActionMap playerInputMap;
    private InputAction INP_movement, INP_look, INP_jump, INP_teleport;
    private PlayerInput playerInput;

    private GameHandler game;

    private bool canMove = true;
    public bool canJump = false;
    private bool teleportable = false;
    private Vector3 teleportPos;

    // Start is called before the first frame update
    void Start()
    {
        //input setup
        playerInput = GetComponentInChildren<PlayerInput>();
        inputAsset = playerInput.actions;
        playerInputMap = inputAsset.FindActionMap("PlayerInGame");
        INP_movement = playerInputMap.FindAction("Movement");
        INP_look = playerInputMap.FindAction("Look");
        INP_jump = playerInputMap.FindAction("Jump");
        INP_teleport = playerInputMap.FindAction("Teleport");

        playerInput.camera = GetComponentInChildren<Camera>();
        GetComponents();

        anim = GetComponentInChildren<Animator>();

        if (gamepad != null)
        {
            // Player is using a gamepad
            Debug.Log("using gamepad");
        }

        if (keyboard != null)
        {
            // Player is using a keyboard
            Debug.Log("using keyboard");
        }

        if (mouse != null)
        {
            // Player is using a mouse
       

            Debug.Log("using mouse");
        }
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            MouseLook();
            Movement();

            if(canJump)
            {
                Jump();
            }  
        }
    }

    private void MouseLook()
    {
        Vector2 look = new Vector2();
        //if (mouse != null)
        //{
        //    //float mouseX = Input.GetAxis("Horizontal") * mouseSensitivity * Time.deltaTime;
        //    //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //    //Debug.Log(mouse.position.ReadValue());
        //    float mouseX = (mouse.position.x.ReadValue() - (Screen.width/2)) * mouseSensitivity * Time.deltaTime;
        //    float mouseY = (mouse.position.y.ReadValue() - (Screen.height/2))* mouseSensitivity * Time.deltaTime;
        //    look = new Vector2(mouseX, mouseY);

        //    playerCamera.localRotation = Quaternion.Euler(xRotationCamera, 0f, 0f);
        //}
        
        
        look = INP_look.ReadValue<Vector2>();
        look = look * mouseSensitivity;

        xRotationCamera -= look.x;
        xRotationCamera = Mathf.Clamp(xRotationCamera, -30f, 0f);

        //playerCamera.localRotation = Quaternion.Euler(xRotationCamera, 0f, 0f);
        gameObject.transform.Rotate(Vector3.up * look.x);
    }

    private void Movement()
    {
        Vector2 movement = new Vector2();
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        movement = INP_movement.ReadValue<Vector2>();
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        if(movement==new Vector2(0,0))
        {
            //Debug.Log("still");
            anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
        }
        
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    private void Jump()
    {
        if (controller.isGrounded && playerVelocity.y <= 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                source.PlayOneShot(jump);
            */
            if (INP_jump.WasPerformedThisFrame())
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                source.PlayOneShot(jump);
            }
        }

        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            playerVelocity.y = 0;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible")
        {
            score.SetScore(5);

            source.PlayOneShot(eatCarrot);

            game.SetCarrotAmount(-1);

            Destroy(other.gameObject);
        }

        if (other.tag == "Hole")
        {
            teleportPos = other.GetComponent<TeleportPlayer>().teleportTarget.transform.position;

            teleportable = true;

            popup.text = "Press 'E' to Enter";
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hole")
        {
            teleportable = false;

            popup.text = "";
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public void SetCanJump(bool value)
    {
        canJump = value;
    }

    private void GetComponents()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Camera").transform;
        //source = GetComponent<AudioSource>();
        score = GetComponent<ScoreScript>();
        //game = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }
}
