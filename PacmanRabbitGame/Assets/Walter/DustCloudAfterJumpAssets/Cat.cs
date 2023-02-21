using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

	Rigidbody2D rb;
	float dirX, moveSpeed = 5f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		dirX = Input.GetAxisRaw ("Horizontal") * moveSpeed;
		if (Input.GetButtonDown ("Jump"))
			rb.AddForce (Vector2.up * 500f);
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector2 (dirX, rb.velocity.y);
	}

}
