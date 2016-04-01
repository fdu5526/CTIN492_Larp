using UnityEngine;
using System.Collections;

public class Fish : Physics2DBody {

	float size;
	float speed;
	AudioSource[] audios;
	Player player;

	// Use this for initialization
	protected override void Awake () {
		base.Awake();

		size = UnityEngine.Random.Range(0.4f, 1.3f);
		transform.localScale = new Vector2(size, size);

		speed = UnityEngine.Random.Range(1.5f, 4f);
		audios = GetComponents<AudioSource>();
		player = GameObject.Find("Player").GetComponent<Player>();
	}

	IEnumerator AddMoney (Harpoon h) {
		int amount = (int)(size * 5f) + (int)UnityEngine.Random.Range(1f, 2f);
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
