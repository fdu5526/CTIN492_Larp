using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Collider2D))]
public class Physics2DBody : MonoBehaviour {

	protected Rigidbody2D rigidbody2d;
	protected Collider2D collider2d;

	// Use this for initialization
	protected virtual void Awake () {
		rigidbody2d = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
	}
}
