using UnityEngine;
using System.Collections;

public class Whale : Physics2DBody {

	float speed;
	AudioSource[] audios;
	Player player;

	// Use this for initialization
	protected override void Awake () {
		base.Awake();
		speed = 2f;
		audios = GetComponents<AudioSource>();
		player = GameObject.Find("Player").GetComponent<Player>();
	}

	IEnumerator AddMoney (Harpoon h) {
		int amount = (int)UnityEngine.Random.Range(40f, 80f);
		player.AddMoney(amount);
		h.Emit(amount);
		for (int i = 0; i < amount; i++) {
			if (!audios[3].isPlaying) {
				audios[3].Play();
			}
			yield return 1;
		}

	}

	void OnCollisionEnter2D (Collision2D coll) {
		Harpoon h = coll.gameObject.GetComponent<Harpoon>();
		if (h != null) {
			int r = (int)UnityEngine.Random.Range(0f, 3f);
			audios[r].Play();
			StartCoroutine(AddMoney(h));
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 v = rigidbody2d.velocity;
		v.x = -speed;
		rigidbody2d.velocity = v;
	}
}
