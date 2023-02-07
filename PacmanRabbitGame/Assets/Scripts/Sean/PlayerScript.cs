using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Movement
    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 1f;
    private float gravity = -9.81f;

    //Camera Movement
    private float mouseSensitivity = 1000f;
    private Transform playerCamera;
    private float xRotationCamera = 0f;

    //Score 
    private ScoreScript score;

    //Sound
    private AudioSource source;
    public AudioClip jump;
    public AudioClip eatCarrot;

    //Teleport Stuff
    public Transform EntranceHole1;
    public Transform EntranceHole2;
    public Transform ExitHole1;
    public Transform ExitHole2;
    private GameObject player;
    private bool hasTeleported = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GetComponents();
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
        Movement();
        Jump();

        if(Input.GetKey(KeyCode.Keypad1))
        {
            transform.position = EntranceHole1.position;
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            transform.position = ExitHole1.position;
        }

        if (Input.GetKey(KeyCode.Keypad3))
        {
            transform.position = EntranceHole2.position;
        }

        if (Input.GetKey(KeyCode.Keypad4))
        {
            transform.position = ExitHole2.position;
        }

    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotationCamera -= mouseY;
        xRotationCamera = Mathf.Clamp(xRotationCamera, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotationCamera, 0f, 0f);
        gameObject.transform.Rotate(Vector3.up * mouseX);
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
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
            if (Input.GetKeyDown(KeyCode.Space))
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
        if(other.tag == "Collectible")
        {
            score.SetScore(10);

            source.PlayOneShot(eatCarrot);

            Destroy(other.gameObject);
        }

        //if (other.tag == "Hole")
        //{
        //    if (other.name == "Hole Entrance1")
        //    {
        //        player.transform.position = new Vector3(0, 5, 0);

        //        Debug.Log("Hole1");
        //    }

        //    if (other.name == "Hole Entrance2")
        //    {
        //        transform.position = ExitHole2.transform.position;
        //    }

        //    if (other.name == "HoleExit1")
        //    {
        //        transform.position = EntranceHole1.transform.position;
        //    }

        //    if (other.name == "HoleExit2")
        //    {
        //        transform.position = EntranceHole2.transform.position;
        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hole")
        {
            if (collision.gameObject.name == "Hole Entrance1")
            {
                player.transform.position = new Vector3(0, 5, 0);

                Debug.Log("Hole1");
            }

            if (collision.gameObject.name == "Hole Entrance2")
            {
                transform.position = ExitHole2.transform.position;
            }

            if (collision.gameObject.name == "HoleExit1")
            {
                transform.position = EntranceHole1.transform.position;
            }

            if (collision.gameObject.name == "HoleExit2")
            {
                transform.position = EntranceHole2.transform.position;
            }
        }
    }

    private void GetComponents()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Camera").transform;
        score = GetComponent<ScoreScript>();
        source = GetComponent<AudioSource>();
        player = gameObject;
    }

    public bool GetHasTeleported()
    {
        return hasTeleported;
    }

    public bool SetHasTeleported(bool yes)
    {
        return hasTeleported = yes;
    }
}
