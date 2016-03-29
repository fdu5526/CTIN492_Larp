using UnityEngine;
using System.Collections;

public class Whale : Physics2DBody {

	float speed;
	AudioSource[] audios;
	ParticleSystem bloodPS;

	// Use this for initialization
	void Awake () {
		base.Awake();
		speed = 2f;
		audios = GetComponents<AudioSource>();
		bloodPS = transform.Find("BloodPS").GetComponent<ParticleSystem>();
	}

	void OnCollisionEnter2D (Collision2D coll) {
		int l = coll.gameObject.layer;
		if (l == Global.LayerPlayer) {
			int r = (int)UnityEngine.Random.Range(0f, 3f);
			audios[r].Play();

			bloodPS.GetComponent<ParticleSystem>().Play();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody2d.velocity = Vector2.left * speed;
	}
}
