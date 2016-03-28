using UnityEngine;
using System.Collections;

public class WaterAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animator>().speed = UnityEngine.Random.Range(0.5f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
