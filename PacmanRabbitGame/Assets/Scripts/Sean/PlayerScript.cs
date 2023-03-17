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
    private float mouseSensitivity = 0.5f;
    private Transform playerCamera;
    private float xRotationCamera = 0f;

    //Score 
    private ScoreScript score;

    //Sound
    private AudioSource source;
    public AudioClip jump;
    public AudioClip eatCarrot;

    //New input stuff

    [SerializeField]
    private InputActionReference INP_movement,INP_look,INP_jump,INP_teleport;
    private Gamepad gamepad = Gamepad.current;
    private Keyboard keyboard = Keyboard.current;
    private Mouse mouse = Mouse.current;

    private GameHandler game;

    // Start is called before the first frame update
    void Start()
    {
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
        MouseLook();
        Movement();
        Jump();
    }

    private void MouseLook()
    {
        Vector2 look = new Vector2();
        if (mouse != null)
        {
            //float mouseX = Input.GetAxis("Horizontal") * mouseSensitivity * Time.deltaTime;
            //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            //Debug.Log(mouse.position.ReadValue());
            float mouseX = (mouse.position.x.ReadValue() - (Screen.width/2)) * mouseSensitivity * Time.deltaTime;
            float mouseY = (mouse.position.y.ReadValue() - (Screen.height/2))* mouseSensitivity * Time.deltaTime;
            look = new Vector2(mouseX, mouseY);

            playerCamera.localRotation = Quaternion.Euler(xRotationCamera, 0f, 0f);
        }
        
        look = INP_look.action.ReadValue<Vector2>();

        
        
        xRotationCamera -= look.y;
        xRotationCamera = Mathf.Clamp(xRotationCamera, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotationCamera, 0f, 0f);
        gameObject.transform.Rotate(Vector3.up * look.x);
    }

    private void Movement()
    {
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        Vector2 movement = INP_movement.action.ReadValue<Vector2>();
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
            if (INP_jump.action.WasPerformedThisFrame())
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
            score.SetScore(10);
            
            source.PlayOneShot(eatCarrot);

            game.SetCarrotAmount(-1);

            Destroy(other.gameObject);
        }
    }

    private void GetComponents()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Camera").transform;
        source = GetComponent<AudioSource>();
        score = GetComponent<ScoreScript>();
        game = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }
}
