using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {
	
	int costS = 250;
	int costR = 250;

	void Update(){
	}

	void OnGUI () {
		string e = ": " + ((Ship.engine + 1) * 50).ToString ();
		string w = ": " + ((Ship.weapon + 1) * 50).ToString ();
		string h = ": " + ((Ship.health + 1) * 50).ToString ();
		string s = ": " + costS.ToString();


		if (gameContent.encounterInt == 7) {
			
			GUI.Box (new Rect (Screen.width * 0.45f, Screen.height * 0.05f, Screen.width * 0.5f, Screen.height * 0.75f), "");
			if (Ship.engine == 10) {
				e = "";
			}
			if (Ship.weapon == 10) {
				w = "";
			}
			if (Ship.health == 10) {
				h = "";
			}
			//************engine
			if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.42f, Screen.width * 0.1f, Screen.height * 0.1f), "Engine" + e)) {
				if (gameContent.scrap > (Ship.engine + 1) * 50) {
					if (Ship.engine < 10) {
						Ship.engine++;
						gameContent.scrap = gameContent.scrap - Ship.engine * 50;
					}
				}
			}
			//************engine
			//************weapon
			if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.53f, Screen.width * 0.1f, Screen.height * 0.1f), "Weapon" + w)) {
				if (gameContent.scrap > (Ship.weapon + 1) * 50) {
					if (Ship.weapon < 10) {
						Ship.weapon++;
						gameContent.scrap = gameContent.scrap - Ship.weapon * 50;
					}
				}
			}
			//************weapon
			//************health
			if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.64f, Screen.width * 0.1f, Screen.height * 0.1f), "Health" + h)) {
				if (gameContent.scrap > (Ship.health + 1) * 50) {
					if (Ship.health < 10) {
						gameContent.maxHealth = gameContent.maxHealth + 50;
						gameContent.health = gameContent.health + 50;
						Ship.health++;
						gameContent.scrap = gameContent.scrap - Ship.health * 50;
					}
				}
			}
			//************health
			//************repair
			if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.1f, Screen.width * 0.1f, Screen.height * 0.15f), "Repair: 5")) {
				if (gameContent.scrap > 10 && gameContent.health < gameContent.maxHealth - 5) {
					gameContent.health = gameContent.health + 5;
					gameContent.scrap = gameContent.scrap - 5;
				}
			}
			if (GUI.Button (new Rect (Screen.width * 0.58f, Screen.height * 0.1f, Screen.width * 0.1f, Screen.height * 0.15f), "Repair: 25")) {
				if (gameContent.scrap > 50 && gameContent.health < gameContent.maxHealth - 50) {
					gameContent.health = gameContent.health + 25;
					gameContent.scrap = gameContent.scrap - 25;
				}
			}
			if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.26f, Screen.width * 0.21f, Screen.height * 0.15f), "Full Repair: " + (gameContent.maxHealth - gameContent.health))) {
				if (gameContent.scrap > (gameContent.maxHealth - gameContent.health) * 2 && gameContent.health < gameContent.maxHealth) {
					gameContent.scrap = gameContent.scrap - (gameContent.maxHealth - gameContent.health);
					gameContent.health = gameContent.health + (gameContent.maxHealth - gameContent.health);
				}
			}
			//************repair
			//***********shield
			if (GUI.Button (new Rect (Screen.width * 0.7f, Screen.height * 0.1f, Screen.width * 0.1f, Screen.height * 0.15f), "Shield: " + costS)) {
				if (gameContent.scrap > costS && Ship.shield < 3) {

					Ship.shield++;
					gameContent.scrap = gameContent.scrap - costS;
					costS = costS * Ship.shield;

				}

			}

			//***********shield
			//***********rocket
			if (GUI.Button (new Rect (Screen.width * 0.7f, Screen.height * 0.26f, Screen.width * 0.1f, Screen.height * 0.15f), "Rocket: " + costR)) {
				if (gameContent.scrap > costR && Ship.rocket < 3) {
					Ship.rocket++;
					gameContent.scrap = gameContent.scrap - costR;
					costR = costR * Ship.rocket;
				}
			}
			//***********rocket
			if (GUI.Button (new Rect (Screen.width * 0.04f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Back")) {
				SceneManager.LoadScene ("map");
				gameContent.encounterInt = 0;
			}
		}
	}
}
