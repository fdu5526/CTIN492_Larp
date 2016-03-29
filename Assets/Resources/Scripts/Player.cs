using UnityEngine;
using System.Collections;

public class Player : Physics2DBody {

	GameObject harpoonCenter;
	GameObject currentHarpoon;
	Vector2 harpoonLocalPos = new Vector2(1.349f, 0.14f);

	Timer harpoonReloadTimer;

	// Use this for initialization
	void Awake () {
		base.Awake();
		harpoonCenter = transform.Find("HarpoonCenter").gameObject;
		harpoonReloadTimer = new Timer(2f);

		MakeNewHarpoon();
	}

	bool HasHarpoon { get { return currentHarpoon != null; } }


	void MakeNewHarpoon () {
		Debug.Assert(!HasHarpoon);

		harpoonCenter.transform.eulerAngles = Vector3.zero;
		currentHarpoon = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Harpoon"));
		currentHarpoon.transform.parent = harpoonCenter.transform;
		currentHarpoon.transform.localPosition = harpoonLocalPos;
	}

	// shoot harpoon, but please have a harpoon to shoot first
	void ShootHarpoon (Vector2 direction) {
		Debug.Assert(HasHarpoon);

		currentHarpoon.GetComponent<Harpoon>().Fire(direction);
		currentHarpoon = null;
	}


	// Update is called once per frame
	void FixedUpdate () {
		
	}


	
	// Update is called once per frame
	void Update () {

		// make harpoon face pointing direction
		Vector3 m = Input.mousePosition;
		m.z = 10f;
		Vector2 mouseD = (Vector2)(Camera.main.ScreenToWorldPoint(m)) - 
										 (Vector2)harpoonCenter.transform.position;
		float angle = Global.Angle(Vector2.right, mouseD);
		harpoonCenter.transform.eulerAngles = new Vector3(0f, 0f, -angle);


		// if mouse button up and we have a harpoon, shoot it
		if (Input.GetMouseButtonUp(0) && HasHarpoon) {
			ShootHarpoon(mouseD);
			harpoonReloadTimer.Reset();
		}

		if (!HasHarpoon && harpoonReloadTimer.IsOffCooldown) {
			MakeNewHarpoon();
		}

	}
}
