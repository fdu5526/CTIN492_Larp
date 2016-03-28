using UnityEngine;
using System.Collections;

public class Harpoon : Physics2DBody {

	int maxLifespan;
	int lifespan;
	GameObject player;
	AudioSource[] audios;

	// Use this for initialization
	protected override void Awake () {
		base.Awake();
		player = GameObject.Find("Player");
		maxLifespan = 60;
		lifespan = maxLifespan;
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
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
