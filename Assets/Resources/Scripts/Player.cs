using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	GameObject harpoonCenter;
	GameObject currentHarpoon;
	Vector2 harpoonLocalPos = new Vector2(1.349f, 0.14f);

	int moneyCount;
	Text moneyText;
	Text spendText;
	Text tickText;
	Text gainText;

	Timer harpoonReloadTimer;
	Timer slowBleedTimer;
	Timer textFlashRedTimer;
	bool over = false;

	// Use this for initialization
	void Awake () {
		moneyCount = 100;
		harpoonCenter = transform.Find("HarpoonCenter").gameObject;
		moneyText = GameObject.Find("Canvas/Text").GetComponent<Text>();
		spendText = GameObject.Find("Canvas/SpendText").GetComponent<Text>();
		gainText = GameObject.Find("Canvas/GainText").GetComponent<Text>();
		tickText = GameObject.Find("Canvas/tickText").GetComponent<Text>();
		spendText.enabled = false;
		harpoonReloadTimer = new Timer(1f);
		slowBleedTimer = new Timer(1f);
		slowBleedTimer.Reset();
		textFlashRedTimer = new Timer(0.5f);
		MakeNewHarpoon();
	}

	bool HasHarpoon { get { return currentHarpoon != null; } }


	IEnumerator AddMoneyAsync (int amount) {
		gainText.text = "+ $" + amount;
		gainText.enabled = true;
		for (int i = 0; i < amount; i++) {
			moneyText.color = Color.green;
			slowBleedTimer.Reset();
			moneyCount += 1;
			moneyText.text = "$" + moneyCount;
			yield return 1;
		}
	}

	public void AddMoney (int amount) {
		textFlashRedTimer.Reset();
		StartCoroutine(AddMoneyAsync(amount));
	}

	void SubtractMoney (int amount) {
		moneyCount -= amount;
		if (amount > 1) {
			spendText.text = "- $" + amount;
			spendText.enabled = true;
		} else {
			tickText.text = "- $" + amount;
			tickText.enabled = true;
		}

		moneyText.text = "$" + moneyCount;
		moneyText.color = Color.red;;
		textFlashRedTimer.Reset();
	}


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
		if (over) {
			return;
		}
		if (slowBleedTimer.IsOffCooldown) {
			SubtractMoney(2);
			slowBleedTimer.Reset();
		} else if (textFlashRedTimer.IsOffCooldown) {
			moneyText.text = "$" + moneyCount;
			moneyText.color = Color.white;
			spendText.enabled = false;
			gainText.enabled = false;
			tickText.enabled = false;
			moneyText.fontSize = 40;
		}

		if (moneyCount <= 0) {
			over = true;
			GameOver();
		} else if (moneyCount >= 2000) {
			GameWin();
		}
	}

	void GameWin () {
		GetComponent<AudioSource>().Stop();
		GameStart g = GameObject.Find("LevelManager").GetComponent<GameStart>();
		g.enabled = true;
		g.Win();
	}

	void GameOver () {
		GetComponent<AudioSource>().Stop();
		GameStart g = GameObject.Find("LevelManager").GetComponent<GameStart>();
		g.enabled = true;
		g.Lose();
	}
	
	// Update is called once per frame
	void Update () {
		if (over) {
			return;
		}

		// make harpoon face pointing direction
		Vector3 m = Input.mousePosition;
		m.z = 10f;
		Vector2 mouseD = (Vector2)(Camera.main.ScreenToWorldPoint(m)) - 
										 (Vector2)harpoonCenter.transform.position;
		float angle = Global.Angle(Vector2.right, mouseD);
		harpoonCenter.transform.eulerAngles = new Vector3(0f, 0f, -angle);


		// if mouse button up and we have a harpoon, shoot it
		if (Input.GetMouseButtonUp(0) && HasHarpoon) {
			SubtractMoney(10);
			ShootHarpoon(mouseD);
			harpoonReloadTimer.Reset();
		}

		if (!HasHarpoon && harpoonReloadTimer.IsOffCooldown) {
			MakeNewHarpoon();
		}

	}
}
