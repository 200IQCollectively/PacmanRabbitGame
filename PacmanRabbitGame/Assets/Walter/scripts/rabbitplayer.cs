using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class rabbitplayer : MonoBehaviour
{
    [SerializeField] GameObject dustCloud;
    [SerializeField]
    private float jumpButtonGracePeriod;
    [SerializeField]
    private Transform currentpoint;
    public foxcollider foxy;
    private Vector3 movedirection;
    private Vector3 velocity;
    public CharacterController controller;
    private Animator anim;
    private float verticalvelocity;
    private float jumpforce=10.0f;
    private Vector3 playerVelocity;
    private float playerSpeed = 5.0f;
    private float jumpHeight = 1f;
    //private float gravity = -9.81f;
    //[SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    //private float jumpHeight = 1f;
    [SerializeField]
    private float jumpSpeed;
	 private float ySpeed;
	 private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;
private GameObject[] coins;
//private ScoreScript scorer;
    public Transform cam;
    public float speed = 70f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public PlayerManager age;
   // public GameObject advancer;
    //  public GameUI invest;
   
 //   public LevelUnlockSystem.GameUI invest=new LevelUnlockSystem.GameUI();
//private GameManager arranger;
int i;
 int scener;
  

    void Start()
    {
           Time.timeScale = 1;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
       //scorer = GetComponent<ScoreScript>();
        originalStepOffset=controller.stepOffset;
        Cursor.visible = true;
        Screen.lockCursor = false;
       // cam=age.player.transform;
     


    }


    // Update is called once per frame
    void Update()
    {
        Move();
       
        coins=GameObject.FindGameObjectsWithTag("Collectible");
    int coiner=coins.Length;
    scener = SceneManager.GetActiveScene ().buildIndex;
    if(coiner == 0 )
    {
        if(scener<8)
        {
          foxy.setfoxesoff();
       age.Advancement();
        }
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //  invest.GameOver(3);
            //  GameUI.Instancer.GameOver(3);
            
           
           
           // advancer.SetActive(true);
             //Time.timeScale = 0;
              
              //Time.timeScale = 1;
            if(scener==8)
            {
                foxy.setfoxesoff();
             age.Gamecompleted();
            }
           // LevelUnlockSystem.LevelSystemManager.Instance.LevelComplete(3);
    }
    
      

    }

    public void Respawn()
    {
        controller.enabled = true;
        anim.ResetTrigger("hasfallen");
    }
   public void firstdeath()
    {
        anim.SetBool("IsRisen", true);
    }
    

    public void death()
    {
        
        anim.SetTrigger("falldownanddie");
        this.enabled = false;
        controller.enabled = false;
        foxy.killrabbitanimation();
       
        
      // FindObjectOfType<GameManager>().Endgame();
    }

    public void dead()
    {
        
        this.enabled = false;
        controller.enabled = false;
        anim.SetTrigger("falldownanddie");
        //this.enabled = false;
        //controller.enabled = false;
        
    }
    public void notdead()
    {
        
        anim.SetTrigger("riseupandlive");
        this.enabled = true;
        controller.enabled = true;
        
    }
      public void deathstopped()
    {
        
        anim.ResetTrigger("hasfallen");
        this.enabled = true;
        controller.enabled = true;
        //foxy.killrabbitanimation();
       
        
      // FindObjectOfType<GameManager>().Endgame();
    }

    private void Move()
    {
       
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        

            if (direction.magnitude >= 0.1f)
            {

                

                Run();
                Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {

                Idle();
            }

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (controller.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            controller.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            anim.SetBool("IsGrounded", true);
            isGrounded = true;
            anim.SetBool("IsJumping", false);
            isJumping = false;
            anim.SetBool("IsFalling", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                
                ySpeed = jumpSpeed;
                anim.SetBool("IsJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
          // controller.stepOffset = 0;
            anim.SetBool("IsGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                anim.SetBool("IsFalling", true);
            }
        }
        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("IsMoving", true);

        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        
            velocity.y = ySpeed;
            controller.Move(velocity * Time.deltaTime);
      
    }

    public void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
      //  GameObject.FindGameObjectWithTag("cameraon").SetActive(true);
       // GameObject.FindGameObjectWithTag("magiccamera").SetActive(false);
        anim.SetFloat("Wolfmotion", 0, 0.1f, Time.deltaTime);
    }

  

    public void Run()
    {
       // GameObject.FindGameObjectWithTag("magiccamera").SetActive(true);
    //   GameObject.FindGameObjectWithTag("cameraon").SetActive(false);
     
        anim.SetFloat("Speed", 0.25f, 0.1f, Time.deltaTime);
        anim.SetFloat("Wolfmotion", 0.5f, 0.1f, Time.deltaTime);
    }
    public void Eat()
    {
        anim.SetTrigger("Eat");
    }

    public void diealittle()
    {
         anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
/*
  public void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Collectible")
        {
            scorer.SetScore();
        Destroy(other.gameObject);
       
       
        }
    }
    */
  
    public void ResetState()
    {
        //enabled = true;
        //spriteRenderer.enabled = true;
        //collider.enabled = true;
        //deathSequence.enabled = false;
        //deathSequence.spriteRenderer.enabled = false;
        //movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
    //    enabled = false;
    //    spriteRenderer.enabled = false;
      //  collider.enabled = false;
        //movement.enabled = false;
        //deathSequence.enabled = true;
        //deathSequence.spriteRenderer.enabled = true;
        //deathSequence.Restart();
    }
}
