using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabbitmovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float movespeed;
    [SerializeField]private float runspeed;
    private Vector3 movedirection;
    private Vector3 velocity;
    private CharacterController controller;
    private Animator anim;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float  groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    void Start()
    {
        controller=GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move ()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if(isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }
        float moveZ = Input.GetAxis("Vertical");
        movedirection = new Vector3(0, 0, moveZ);
        movedirection = transform.TransformDirection(movedirection);    
        //movedirection *= runspeed;
        if(isGrounded)
        {

            if (movedirection != Vector3.zero)
            {
                Run();
            }
            else
            {
                Idle();
            }
            movedirection *= movespeed;
        }
      
        
        controller.Move(movedirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        movespeed = runspeed;
        anim.SetFloat("Speed", 0.5f,0.1f,Time.deltaTime);
    }
}
