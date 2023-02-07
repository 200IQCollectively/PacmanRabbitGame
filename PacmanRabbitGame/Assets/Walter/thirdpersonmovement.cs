using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdpersonmovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    float speed = 6.0f;
    public Transform cam;
    public float turnsmoothtime = 0.1f;
    float turnsmoothvelocity;
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction=new Vector3(horizontal,0f, vertical).normalized;
        if( direction.magnitude>=0.1f)
        {
            float targetangle=Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg+ cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f)*Vector3.forward;
            controller.Move(movedir * speed * Time.deltaTime);
        }
        
    }

   
}
