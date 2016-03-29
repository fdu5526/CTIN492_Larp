using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	float minTime = 1f;
	float maxTime = 3f;

	Timer spawnTimer;

	// Use this for initialization
	void Start () {
		spawnTimer = new Timer(NewSpawnTime);
	}

	float NewSpawnTime { get { return UnityEngine.Random.Range(minTime, maxTime); } }

	public void Spawn () {
		GameObject g;
		if (UnityEngine.Random.Range(0f, 1f) <= 0.015f) {
			g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Whale"));
		} else {
			g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Fish"));
		}
		
		Vector2 p = (Vector2)transform.position;
		p.y += UnityEngine.Random.Range(-1f, 1f);
		g.transform.position = p;
	}


	void FixedUpdate () {
		if (spawnTimer.IsOffCooldown) {
			Spawn();
			spawnTimer.CooldownTime = NewSpawnTime;
			spawnTimer.Reset();
		}
	}
}
