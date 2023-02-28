using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public TextMeshProUGUI popup;
    public Vector3 teleportPos;
    private bool teleportable;

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

        if (Input.GetKeyDown(KeyCode.E) && teleportable)
        {
            transform.position = teleportPos;

            teleportable = false;
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
        if (other.tag == "Collectible")
        {
            score.SetScore(10);

            source.PlayOneShot(eatCarrot);

            Destroy(other.gameObject);
        }

        if (other.tag == "Hole")
        {
            popup.text = "Press 'E' to Enter";

            teleportPos = other.GetComponent<TeleportPlayer>().teleportTarget.transform.position;

            teleportable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hole")
        {
            popup.text = "";

            teleportable = false;
        }
    }

    private void GetComponents()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Camera").transform;
        score = GetComponent<ScoreScript>();
        source = GetComponent<AudioSource>();
    }
}
