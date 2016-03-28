using UnityEngine;
using System.Collections;

public class Whale : Physics2DBody {

	float speed;

	// Use this for initialization
	void Awake () {
		base.Awake();
		speed = 2f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody2d.velocity = Vector2.left * speed;
	}
}
