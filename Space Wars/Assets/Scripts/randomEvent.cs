using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class randomEvent : MonoBehaviour {
	int scrapGiven;
	int text;
	int rand;
	int damage;
	bool pressed = false;
	bool pressed2 = false;
	bool done = false;


	private string[] randomEncounter = new string[]{
		"You come accross a ship that appears to be in trouble. Do you want to approach?",
		"Your radio picks up a destress signal from near by. do you want to investigate?",
	};
	private string[] randomEncounter1 = new string[]{
		"The ship had stalled and they couldn't start their engine. You send and enginer to fix his engine and he paid you: ",
		"The ship had ran out of fuel so you give him some of yours. He paid you: ",
	};
	private string[] randomEncounter2 = new string[]{
		"It was an ambush the ship fired at you. You fired a rocket and the ship exploded. You took ",
		"When you reach the location of the signal you were greeted by a fleet of enemy ships. You managed to escape but you took ",
	};


	// Use this for initialization
	void Start () {
		scrapGiven = Random.Range (150, 300);
		damage = Random.Range (150, 200);
		text = Random.Range (0, randomEncounter.Length);
		rand = Random.Range (1, 3);
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (gameContent.encounterInt == 4 || gameContent.encounterInt == 5) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Backgrounds.background);
			if (pressed == false) {
				if (GUI.Button (new Rect (Screen.width * 0.52f, Screen.height * 0.52f, Screen.width * 0.05f, Screen.height * 0.05f), "yes")) {
					pressed = true;
				}
				if (GUI.Button (new Rect (Screen.width * 0.42f, Screen.height * 0.52f, Screen.width * 0.05f, Screen.height * 0.05f), "no")) {
					pressed = true;
					done = true;
				}
				GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), randomEncounter [text]);
			}

			if (pressed == true && pressed2 == false && done == false) {
				if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.52f, Screen.width * 0.05f, Screen.height * 0.05f), "ok")) {
					pressed2 = true;
					done = true;
					if (rand == 1) {
						gameContent.scrap = gameContent.scrap + scrapGiven;
						gameContent.totalScrap = gameContent.totalScrap + scrapGiven;
					}
					if (rand != 1) {
						gameContent.health = gameContent.health - damage;
					}
				}
				if (rand == 1) {
					GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), randomEncounter1 [text] + scrapGiven + " scrap");

				} else {
					GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), randomEncounter2 [text] + damage + " damage");
				}
			}

			if (gameContent.health <= 0) {
				gameContent.score = gameContent.totalScrap + (gameContent.health * 2) + gameContent.pointCount;
				if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * 0.5f, Screen.width * 0.1f, Screen.height * 0.05f), "Quit")) {
					Application.Quit ();
				}
				GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), "Your ship was destoryed.                  Your score is: " + gameContent.score);
			}

			if (done == true && gameContent.health > 0) {
				if (GUI.Button (new Rect (Screen.width * 0.04f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Back")) {
					SceneManager.LoadScene ("map");
					gameContent.encounterInt = 0;
				}
			}
		}
	}

}
