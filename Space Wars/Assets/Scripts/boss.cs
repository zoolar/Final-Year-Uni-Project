using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class boss : MonoBehaviour {

	bool attack = false;
	bool pressed = false;
	float timer = 0.0f;
	int enemyHealth = 2000;
	public Texture bosss;
	public Texture shieldTex;
	public Texture laser;
	public Texture rocket;
	public Texture rocketE;


	//laser texture and movement
	float location = 0.2f;
	float speed = 0.05f;

	//fire time + hit
	float time = 1.0f ;
	float timeStamp;
	float hit = 11;

	//fire time + hit (enemy ship)
	float locationE1 = 0.8f;
	float timeStampE1;
	float hitE1 = 11;
	float locationE2 = 0.8f;
	float timeStampE2;
	float hitE2 = 11;

	// shield player + enemy
	public static bool shield = false;
	bool shieldE = false;
	int shieldCount = 0;
	float cooldown = 10.0f;
	float shieldTE = 10.0f;
	float shieldT = 5.0f;
	float shieldTS;
	float shieldTSE;
	float shieldCD = 5.0f;
	float shieldCDE = 5.0f;

	//rocket splayer + enemy
	float rocketTime = 10.0f;
	bool rocketFire = false;
	float rocketLoc = 0.2f;
	float rocketSpeed = 0.005f;
	float rocketCount;
	float rocketLocE = 0.8f;
	bool rocketFireE = false;
	float rocketCountE;
	int fireCount;

	void Start(){
		shieldT = shieldT * Ship.shield;
		shieldTE = shieldTE * 3;
	}

	void Update () {
		if (gameContent.encounterInt == 9 && attack == true) {
			//******************************** Player Attack
			//user laser fire
			if (Time.time > timeStamp) {
				Sounds.fire = true;
				location = 0.1f;
				timeStamp = Time.time + time;
				for (int i = 0; i < 1; i++) {
					timeStamp = timeStamp + 0.5f;
				}
				hit = Random.Range (0, 10); 
				if (shieldE == true) {
					hit = 10;
				}
			}
			if (location >= 0.8f) {
			} else {
				location = location + speed;
			}
			if (hit <= 6) {
				Sounds.hit = true;
				enemyHealth = enemyHealth - Random.Range (20 * Ship.weapon, 30 * Ship.weapon);
				hit = 11;
			}
			if (hit >= 7 &&  hit <= 10) {
				location = location + speed;
			}

			// if fire true rocket time reset
			if (rocketFire == true) {
				if (rocketLoc >= 0.8f) {
					Sounds.rHit = true;
					rocketFire = false;
					if (Ship.rocket == 1  || Ship.rocket == 2) {
						enemyHealth = enemyHealth - 50 * Ship.rocket;
					}
					else {enemyHealth = enemyHealth - 50 * 5;}
				} else {rocketLoc = rocketLoc + rocketSpeed;}

			}

			//enemy laser fire
			if (Time.time > timeStampE1) {
				locationE1 = 0.8f;
				timeStampE1 = Time.time + time;
				hitE1 = Random.Range (0, 10); //changes depending on player dodge.
				if (shield == true) {
					hitE1 = 10;
				}
			}
			if (locationE1 <= 0.15f) {
			} else {
				locationE1 = locationE1 - speed;}
			if (hitE1 <=6 - (Ship.engine * 0.2f)) { // 6 - test
				gameContent.health = gameContent.health - Random.Range (20, 30);
				hitE1 = 11;
			}
			if (hit >= (7 - (Ship.engine * 0.2f)) && hitE1 <= 10) { // 7 - test
				locationE1 = locationE1 - speed;
			}


			if (Time.time > timeStampE2) {
				locationE2 = 0.8f;
				timeStampE2 = Time.time + time;
				hitE2 = Random.Range (0, 10); //changes depending on player dodge.
				if (shield == true) {
					hitE2 = 10;
				}
			}
			if (locationE2 <= 0.1f) {
			} else {
				locationE2 = locationE2 - speed;}
			if (hitE2 <=6 - (Ship.engine * 0.2f)) { // 6 - test
				gameContent.health = gameContent.health - Random.Range (20, 30);
				hitE2 = 11;
			}
			if (hit >= (7 - (Ship.engine * 0.2f)) && hitE2 <= 10) { // 7 - test
				locationE2 = locationE2 - speed;
			}


			//if enemy health or plqyer health < 0 end attack
			if (enemyHealth <= 0 || gameContent.health <= 0) {
				attack = false;
			}
			// every second decides if enemy fires rockets or uses shield
			if (Time.time > timer){
				fireCount = Random.Range (0, 20);
				shieldCount = (Random.Range (0, 20));
				timer = timer + 1.0f;
			}
			// enemy shield
			if (Time.time > shieldCDE && shieldE == false) {
				if (shieldCount > 10) {
					shieldTSE = Time.time + shieldTE;
					shieldE = true;
				}
			}
			if (shieldE == true) {
				if (Time.time > shieldTSE) {
					shieldCDE = Time.time + cooldown;
					shieldE = false;
				}
			}

			//enemy rocket
			if (Time.time > rocketCountE && rocketFire == false) {
				if (fireCount > 13) {
					rocketFireE = true;
					rocketLocE = 0.8f;
					fireCount = 0;
				}
			}

			if (rocketFireE == true) {
				rocketCountE = Time.time + rocketTime;
				if (rocketLocE <= 0.15f) {
					rocketFireE = false;
					gameContent.health = gameContent.health - 250;
				} else {rocketLocE = rocketLocE - rocketSpeed;}
			}
		}
	}

	void OnGUI(){
		if (gameContent.encounterInt == 9) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Backgrounds.background);
			if (attack == true) {
				if (enemyHealth > 0 && gameContent.health > 0) {
					// player shield
					if (shield == true){
						GUI.DrawTexture (new Rect (Screen.width * 0.05f, Screen.height * 0.35f, Screen.width * 0.3f, Screen.height * 0.4f), shieldTex);
						if (Time.time > shieldTS){
							shieldCD = Time.time + cooldown;
							shield = false;
						}
					}
					if (Ship.shield > 0) {
						if (Time.time > shieldCD && shield == false){
							if (GUI.Button (new Rect (Screen.width * 0.2f, Screen.height * 0.8f, Screen.width * 0.2f, Screen.height * 0.1f), "Shield")) {
								shieldTS = Time.time + shieldT;
								shield = true;
							}
						}
					}

					if (Ship.rocket > 0) {
						if (Time.time > rocketCount && rocketFire == false) {
							if(GUI.Button (new Rect (Screen.width * 0.6f, Screen.height * 0.8f, Screen.width * 0.2f, Screen.height * 0.1f), "rocket: " )){
								Sounds.rFire = true;
								rocketCount = Time.time + rocketTime;
								rocketLoc = 0.2f;
								rocketFire = true;
							}
						}
					}

					if (shield == true) {
						GUI.DrawTexture(new Rect(Screen.width *0.05f, Screen.height * 0.35f, Screen.width *0.35f, Screen.height * 0.38f), shieldTex);
					}
					if (shieldE == true) {
						GUI.DrawTexture(new Rect(Screen.width *0.6f, Screen.height * 0.35f, Screen.width *0.4f, Screen.height * 0.38f), shieldTex);
					}


					GUI.DrawTexture (new Rect (Screen.width * rocketLoc, Screen.height * 0.5f, Screen.width * 0.048f, Screen.height * 0.1f), rocket);
					GUI.DrawTexture (new Rect (Screen.width * rocketLoc, Screen.height * 0.5f, Screen.width * 0.048f, Screen.height * 0.1f), rocket);
					GUI.DrawTexture (new Rect (Screen.width * rocketLocE, Screen.height * 0.5f, Screen.width * 0.048f, Screen.height * 0.1f), rocketE);
					GUI.DrawTexture (new Rect (Screen.width * location, Screen.height * 0.5f, Screen.width * 0.048f, Screen.height * 0.1f), laser);
					GUI.DrawTexture (new Rect (Screen.width * locationE1, Screen.height * 0.49f, Screen.width * 0.052f, Screen.height * 0.1f), laser);
					GUI.DrawTexture (new Rect (Screen.width * locationE2, Screen.height * 0.51f, Screen.width * 0.052f, Screen.height * 0.1f), laser);
					GUI.TextField (new Rect (Screen.width * 0.8f, Screen.width * 0.02f, Screen.width * 0.1f, Screen.height * 0.05f), "Health: " + enemyHealth);
				}
			}
			//textbox about current point + enemy ship
			if (pressed == false) {
				if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.52f, Screen.width * 0.05f, Screen.height * 0.05f), "ok")) {
					pressed = true;
					attack = true;
					rocketCount = Time.time + rocketTime;
					rocketCountE = Time.time + rocketTime;
				}
				GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), "you have reached the enemy fleet command ship. Once this ship is destoryed the enemy fleet wil dispand and the universe will be safe again.");
			}
			if (enemyHealth > 0) {
				GUI.DrawTexture (new Rect (Screen.width * 0.65f, Screen.height * 0.4f, Screen.width * 0.3f, Screen.height * 0.3f), bosss);
			}
			if (shieldE == true) {
				GUI.DrawTexture (new Rect (Screen.width * 0.60f, Screen.height * 0.35f, Screen.width * 0.40f, Screen.height * 0.4f), shieldTex);
			}

			if (gameContent.health <= 0) {
				gameContent.score = gameContent.totalScrap + (gameContent.health * 2) + gameContent.pointCount;
				if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * 0.5f, Screen.width * 0.1f, Screen.height * 0.05f), "Quit")) {
					Application.Quit ();
				}
				GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), "Your ship was destoryed. Your score is: " + gameContent.score);
			}

			if (attack == false && pressed == true && gameContent.health > 0) {
				gameContent.score = gameContent.score * 2;
				if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.52f, Screen.width * 0.05f, Screen.height * 0.05f), "ok")) {
					Application.Quit ();
				}
				GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), "Congratulations! You have destroyed the fleet command ship. Your score is:" + gameContent.score);
		
			}
		}
	}
}
