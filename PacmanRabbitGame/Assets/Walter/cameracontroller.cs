using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float mousesensitivity;
    private Vector3 movdirection;
    private Transform parent;
    void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
       // float mouseX = Input.GetAxis("Mouse X") * mousesensitivity * Time.deltaTime;
        float moveX = Input.GetAxis("Horizontal");
     parent.Rotate(Vector3.up, moveX);
    }
}
