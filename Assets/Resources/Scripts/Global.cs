using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * a bunch of global stuff that everyone needs put into the same place
 * everything in here is jank as hell, do not question
 */
public class Global
{
	public static bool FiftyFifty { get { return UnityEngine.Random.value > 0.5f; } }
	
	// find facing angle based on default sprite direction
	public static float Angle (Vector2 defaultDirection, Vector2 direction) {
		float theta = Vector2.Angle(defaultDirection, direction);
		if (defaultDirection == Vector2.down) {
			theta = direction.x < 0f ? -theta : theta;
		} else if (defaultDirection == Vector2.right) {
  		theta = direction.y > 0f ? -theta : theta;
		} else if (defaultDirection == Vector2.up) {
			theta = direction.x > 0f ? -theta : theta;
		} else if (defaultDirection == Vector2.left) {
  		theta = direction.y < 0f ? -theta : theta;
		} 
		return theta;
	}


	// get the vector at the angle DEGREES to vector v
	public static Vector2 VectorAtAngle (Vector2 v, float degrees) {
		float r = degrees / 180f * Mathf.PI;
		float c = Mathf.Cos(r);
		float s = Mathf.Sin(r);
		return new Vector2(v.x * c + v.y * s, v.x * -s + v.y * c);
	}
}