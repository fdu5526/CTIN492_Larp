using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStart : MonoBehaviour {

	public GameObject opening;
	public GameObject endingLose;
	public GameObject endingWin;

	bool lost;

	// Use this for initialization
	void Awake () {
		lost = false;
		opening.SetActive(true);
		endingLose.SetActive(false);
		endingWin.SetActive(false);
		
		EnableScripts(false);

	}

	void EnableScripts (bool enabled) {
		MonoBehaviour[] m = Object.FindObjectsOfType<Spawner>();
		for (int i = 0; i < m.Length; i++) {
			m[i].enabled = enabled;
		}
		m = Object.FindObjectsOfType<Physics2DBody>();
		for (int i = 0; i < m.Length; i++) {
			m[i].enabled = enabled;
		}
		m = Object.FindObjectsOfType<Player>();
		for (int i = 0; i < m.Length; i++) {
			m[i].enabled = enabled;
		}
	}

	public void Lose () {
		endingLose.SetActive(true);
		lost = true;
		EnableScripts(false);
	}

	public void Win () {
		endingWin.SetActive(true);
		lost = true;
		EnableScripts(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp("space")) {
			if (lost) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			} else {
				opening.SetActive(false);
				EnableScripts(true);
				this.enabled = false;
				Object.FindObjectOfType<Player>().GetComponent<AudioSource>().Play();
			}
			
		}
	}
}
