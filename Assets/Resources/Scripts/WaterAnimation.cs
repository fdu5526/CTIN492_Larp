using UnityEngine;
using System.Collections;

public class WaterAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animator>().speed = UnityEngine.Random.Range(0.25f, 0.75f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
