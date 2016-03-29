using UnityEngine;
using System.Collections;

public class Whale : Physics2DBody {

	float speed;
	AudioSource[] audios;

	// Use this for initialization
	void Awake () {
		base.Awake();
		speed = 2f;
		audios = GetComponents<AudioSource>();
	}

	void OnCollisionEnter2D (Collision2D coll) {
		int l = coll.gameObject.layer;
		if (l == Global.LayerPlayer) {
			int r = (int)UnityEngine.Random.Range(0f, 3f);
			audios[r].Play();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 v = rigidbody2d.velocity;
		v.x = -speed;
		rigidbody2d.velocity = v;
	}
}
