using UnityEngine;
using System.Collections;

public class Harpoon : Physics2DBody {

	int maxLifespan;
	int lifespan;
	GameObject player;
	AudioSource[] audios;

	float speed = 20f;
	bool fired;

	// Use this for initialization
	protected override void Awake () {
		base.Awake();
		player = GameObject.Find("Player");
		maxLifespan = 60;
		lifespan = maxLifespan;
		fired = false;
		audios = GetComponents<AudioSource>();
	}

	// stop moving, and become unhittable
	void HitSomething () {
		rigidbody2d.velocity = Vector2.zero;
		rigidbody2d.angularVelocity = 0f;
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject != null) {
			int l = coll.gameObject.layer;
			//audios[1].Play();
		}
	}

	public void Fire (Vector2 direction) {
		transform.parent = null;
		rigidbody2d.velocity = direction.normalized * speed;
		rigidbody2d.gravityScale = 1f;
		fired = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (fired) {
			float angle = Global.Angle(Vector2.right, rigidbody2d.velocity);
			rigidbody2d.rotation = -angle;
		}
	}
}
