using UnityEngine;
using System.Collections;

public class Harpoon : Physics2DBody {

	AudioSource[] audios;

	float speed = 20f;
	bool firing;
	ParticleSystem bloodPS;

	// Use this for initialization
	protected override void Awake () {
		base.Awake();
		firing = false;
		audios = GetComponents<AudioSource>();
		bloodPS = transform.Find("BloodPS").GetComponent<ParticleSystem>();
		rigidbody2d.isKinematic = true;
	}

	void DestroySelf () {
		collider2d.enabled = false;
		GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 0.2f);	
		bloodPS.transform.parent = null;
		Destroy(this.gameObject, 10f);
	}

	// stop moving, and become unhittable
	void HitSomething () {
		firing = false;
		rigidbody2d.velocity = Vector2.zero;
		rigidbody2d.angularVelocity = 0f;
		Invoke("DestroySelf", 0.1f);
	}

	IEnumerator EmitAsync (int amount) {
		for (int i = 0; i < amount; i++) {
			bloodPS.GetComponent<ParticleSystem>().Emit(1);
			yield return new WaitForSeconds(0.01f);
		}
	}

	public void Emit (int amount) {
		StartCoroutine(EmitAsync(amount));
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (firing && coll.gameObject != null) {
			HitSomething();
		}
	}

	public void Fire (Vector2 direction) {
		transform.parent = null;
		rigidbody2d.velocity = direction.normalized * speed;
		rigidbody2d.gravityScale = 1f;
		firing = true;
		rigidbody2d.isKinematic = false;
		audios[0].Play();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (firing) {
			float angle = Global.Angle(Vector2.right, rigidbody2d.velocity);
			rigidbody2d.rotation = -angle;
		}
	}
}
