using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foxplayer : MonoBehaviour
{
    [SerializeField] GameObject dustCloud;


    private Vector3 movedirection;
    private Vector3 velocity;
    public Vector3 initialdirection,direction;
    private Animator anim;
    private float verticalvelocity;
    private float jumpforce=10.0f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    
    public Transform cam;
    public float speed = 70f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //void Start()
    //{
        
    //   // anim = GetComponentInChildren<Animator>();
    //}


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (isGrounded)
        {
            //verticalvelocity = -gravity * Time.deltaTime;
        
            if (direction.magnitude >= 0.1f)
            {

                //if (Input.GetKey(KeyCode.Space))
                //{
                //    anim.SetTrigger("jumping");
                //    verticalvelocity = jumpforce;
                //    Vector3 movevector = new Vector3(0, verticalvelocity, 0);
                //    controller.Move(movevector * Time.deltaTime);
                //}

                Run();
                Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                //controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {

                Idle();
            }
        }
        velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    public void Eat()
    {
        anim.SetTrigger("Eat");
    }

    public void ResetState()
    {
        //enabled = true;
        //spriteRenderer.enabled = true;
        //collider.enabled = true;
        //deathSequence.enabled = false;
        //deathSequence.spriteRenderer.enabled = false;
        //movement.ResetState();
       // gameObject.SetActive(true);
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
