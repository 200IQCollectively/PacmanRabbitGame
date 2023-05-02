using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //Movement
     //Movement
     [SerializeField] GameObject dustCloud;
       public Material[] material;
    private Animator anim;
    public lower wolf;
    public parent parental;
    public convert converter;
    public CharacterController controller;
    private Vector3 playerVelocity;
    public float playerSpeed = 6.0f;
    [SerializeField] private float jumpHeight = 1f;
    private float gravity = -9.81f;

    //Camera Movement
    private float mouseSensitivity = 0.8f;
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
    private InputActionReference INP_movement,INP_look,INP_jump,INP_teleport, INP_pause;
    private Gamepad gamepad = Gamepad.current;
    private Keyboard keyboard = Keyboard.current;
    private Mouse mouse = Mouse.current;

    //Game Stuff
    private GameHandler game;
    private bool canMove = true;
    public bool canJump = false;
    private int lives = 3;
    private Transform spawn;
    private bool inMenu = false;
    private GameObject Menu;

    //Teleport Stuff
    public TextMeshProUGUI popup;
    private Vector3 teleportPos;
    private bool teleportable;

    //Minimap
    private GameObject minimap;
    private bool isInside = true;

    public float timer;

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
        OpenMenu();
        
        if (canMove)
        {
            if(!inMenu)
            {
                MouseLook();
            }
            
            Movement();

            if(canJump)
            {
                Jump();
            }

            if (INP_teleport.action.WasPerformedThisFrame() && teleportable)
            {
                transform.position = new Vector3(teleportPos.x, teleportPos.y + 1, teleportPos.z);

                teleportable = false;

                canJump = !canJump;

                isInside = !isInside;

                minimap.SetActive(isInside);
            }
        }
    }

    private void OpenMenu()
    {
        //if menu opening button is pressed
        if (INP_pause.action.WasPerformedThisFrame())
        {
            inMenu = !inMenu;
        }

        if (inMenu)
        {
            Menu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
        else
        {
            Menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
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
        
        
        look = INP_look.action.ReadValue<Vector2>();
        
        xRotationCamera -= look.y;
        xRotationCamera = Mathf.Clamp(xRotationCamera, -10f, 25f);

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

        if (controller.isGrounded && playerVelocity.y <= 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    private void Jump()
    {
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
            score.SetScore(5);
            
            source.PlayOneShot(eatCarrot);

            game.SetCarrotAmount(-1);

            Destroy(other.gameObject);
        }

        if (other.tag == "powerupcollectible")
        {
            UpdatePlayer();
           converter.UpdatePlayer();
            score.SetScore(5);
            
            source.PlayOneShot(eatCarrot);

            game.SetCarrotAmount(-1);

            Destroy(other.gameObject);
        }

        if(other.tag == "Hole")
        {
            teleportPos = other.GetComponent<TeleportPlayer>().teleportTarget.transform.position;

            teleportable = true;

            popup.text = "Press 'E' to Enter";
        }

        if(other.tag == "AI")
        {
            PlayerDied();
            deathofrabbit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Hole")
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

    public ScoreScript GetScoreScript()
    {
        return score;
    }

    public Transform SetSpawn(Transform _spawn)
    {
        return spawn = _spawn;
    }

    public void PlayerDied()
    {      
        float resetTime = 2.0f;

        canMove = false;
        lives -= 1;
        timer = resetTime;
        timer -= 1.0f * Time.deltaTime;

        //if (lives <= 0)
        //{
        //    game.EndGame();
        //}

        if (timer <= 0)
        {
            transform.position = spawn.position;
            canMove = true;
        }
    }

    private void GetComponents()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Camera").transform;
        source = GetComponent<AudioSource>();
        score = GetComponent<ScoreScript>();
        game = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        popup = GameObject.Find("MainCanvas").transform.Find("PopupText").GetComponent<TextMeshProUGUI>();

        minimap = GameObject.Find("MainCanvas").transform.Find("Minimap").gameObject;

        Menu = GameObject.Find("MainCanvas").transform.Find("Menu").gameObject;
    }

 public void UpdatePlayer()
   {
            //gameObject.SetActive(false);
            parental.explosioneffect(false);
            parental.rabbitonly(false);
            parental.explosioneffect(true);
           parental.whitewolfonly(true);
           playerSpeed = 20.0f;
          changewolfcolortoblue();
          
      /*


   GameObject[] objectsWithTags = GameObject.FindGameObjectsWithTag("AI");

// Loop through all the objects and turn off a script attached to them
foreach (GameObject objs in objectsWithTags)
{
    // Get the component you want to turn off
    lower script = objs.GetComponent<lower>();
    if (script != null)
    {
        // Turn off the script
        script.enabled = false;
    }
}

   GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("AI");

// Loop through all the objects and turn off a script attached to them
foreach (GameObject obj in objectsWithTag)
{
    // Get the component you want to turn off
    plower script = obj.GetComponent<plower>();
    if (script != null)
    {
        // Turn off the script
        script.enabled = true;
    }
}
*/

            Invoke("backtodefaults", 10.0f);
        
    }


    public void backtodefaults()
    {

        //Invoke("rest", 0.1f);
        parental.whitewolfonly(false);
        parental.explosioneffect(false);
        parental.rabbitonly(true);
       // Invoke("restdone", 0.1f);
       // engage.speed = 60;
        parental.explosioneffect(true);
        playerSpeed = 5.0f;
        changewolfcolorbacktooriginal ();

/*
   GameObject[] objectsWithTags = GameObject.FindGameObjectsWithTag("AI");

// Loop through all the objects and turn off a script attached to them
foreach (GameObject objs in objectsWithTags)
{
    // Get the component you want to turn off
    lower script = objs.GetComponent<lower>();
    if (script != null)
    {
        // Turn off the script
        script.enabled = true;
    }
}

   GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("AI");

// Loop through all the objects and turn off a script attached to them
foreach (GameObject obj in objectsWithTag)
{
    // Get the component you want to turn off
    plower script = obj.GetComponent<plower>();
    if (script != null)
    {
        // Turn off the script
        script.enabled = false;
    }
}

*/




        
    }

    public void playerstate()
    {
        parental.whitewolfonly(false);
        parental.explosioneffect(false);
        parental.rabbitonly(true);

    }
    public void rest()
    {
       // engage.controller.enabled = false;
    }
    public void restdone()
    {
        //engage.controller.enabled = true;
    }

public void changewolfcolortoblue ()
{
  GameObject[] enemies = GameObject.FindGameObjectsWithTag("foxbody");

// Loop through each enemy and change its material
foreach (GameObject enemy in enemies)
{
    Renderer renderer = enemy.GetComponent<Renderer>();
    renderer.material=material[0];
}
}

public void changewolfcolorbacktooriginal ()
{
  GameObject[] enemies = GameObject.FindGameObjectsWithTag("foxbody");

// Loop through each enemy and change its material
foreach (GameObject enemy in enemies)
{
    Renderer renderer = enemy.GetComponent<Renderer>();
    renderer.material=material[1];
}
}

  public void deathofrabbit()
    {
        
        anim.SetTrigger("falldownanddie");
      //  this.enabled = false;
      // controller.enabled = false;
       
      
        
      // FindObjectOfType<GameManager>().Endgame();
    }



}
