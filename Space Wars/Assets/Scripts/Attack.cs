using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour {
	bool attack = false;
	bool pressed = false;
	bool pressed2 = false;
	bool question;
	float timer = 0.0f;
	int enemyHealth = 500;
	int text = 0;
	int enemyShip = 0;
	int scrapGiven;
	public Texture[] enemy;
	public Texture shieldTex;
	public Texture laser;
	public Texture rocket;
	public Texture rocketE;
	private string[] enemyText = new string[]{
		"A fleet of enemy ships are passing, one of them is trailing behind. Do you want to attack?",
		"An enemy ship approaches you. Prepare to attack.",
		"you have fallen behind the enemy fleet, they are attacking you"
	};

	//laser texture and movement
	float location = 0.2f;
	float speed = 0.05f;

	//fire time + hit
	float time = 1.0f ;
	float timeStamp;
	float hit = 11;

	//fire time + hit (enemy ship)
	float locationE = 0.8f;
	float timeStampE;
	float hitE = 11;

	// shield player + enemy
	public static bool shield = false;
	bool shieldE = false;
	int shieldCount = 0;
	float cooldown = 5.0f;
	float shieldTE = 5.0f;
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
		enemyHealth = enemyHealth + Random.Range(0, gameContent.pointCount * 50);
		scrapGiven = Random.Range (330, 520);
		shieldT = shieldT * Ship.shield;
		if (gameContent.pointCount >= 15 && gameContent.pointCount <= 25) {
			shieldTE = shieldTE * 2;
		} else if (gameContent.pointCount > 25) {
			shieldTE = shieldTE * 3;
		}
		text = Random.Range (0, enemyText.Length - 1);
		if (text == 0) {
			question = true;
		} else {
			question = false;
		}
		enemyShip = Random.Range (0, enemy.Length);
		if (gameContent.enemyLine == true) {
			text = 2;
			question = false;
			scrapGiven = 0;
		}
	}
		
	void Update () {
		
		if (gameContent.encounterInt >= 1 && gameContent.encounterInt <=3 && attack == true) {
			//******************************** Player Attack
			//user laser fire
			if (Time.time > timeStamp) {
				Sounds.fire = true;
				location = 0.1f;
				timeStamp = Time.time + time;
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
			if (Time.time > timeStampE) {
				locationE = 0.8f;
				timeStampE = Time.time + time;
				hitE = Random.Range (0, 10); //changes depending on player dodge.
				if (shield == true) {
					hitE = 10;
				}
			}
			if (locationE <= 0.15f) {
			} else {
				locationE = locationE - speed;}
			if (hitE <=6 - (Ship.engine * 0.2f)) { // 6 - test
				gameContent.health = gameContent.health - Random.Range (20 + (gameContent.pointCount * 2), 30 + (gameContent.pointCount * 2));
				hitE = 11;
			}
			if (hit >= (7 - (Ship.engine * 0.2f)) && hitE <= 10) { // 7 - test
				locationE = locationE - speed;
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
				if (gameContent.pointCount >= 5){
					if (shieldCount > 10) {
						shieldTSE = Time.time + shieldTE;
						shieldE = true;
					}
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
				if (gameContent.pointCount >= 10) {
					if (fireCount > 13) {
						rocketFireE = true;
						rocketLocE = 0.8f;
						fireCount = 0;
					}
				}
			}
		
			if (rocketFireE == true) {
				rocketCountE = Time.time + rocketTime;
				if (rocketLocE <= 0.15f) {
					rocketFireE = false;
					print ("rocket fire: " + rocketFireE);
					if (gameContent.pointCount < 15) {
						gameContent.health = gameContent.health - 50;
					}
					if (gameContent.pointCount >= 15 && gameContent.pointCount <= 25) {
						gameContent.health = gameContent.health - 100;
					} else {gameContent.health = gameContent.health - 150;}
				} else {rocketLocE = rocketLocE - rocketSpeed;}
			}
		}
	}
		
	void OnGUI(){
		
		if (gameContent.encounterInt >= 1 && gameContent.encounterInt <= 3) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Backgrounds.background);
			
			if (enemyHealth > 0 && gameContent.health > 0) {
				// player shield
				if (shield == true){
					if (Time.time > shieldTS){
						shieldCD = Time.time + cooldown;
						shield = false;
					}
				}
				if (pressed == true) {
					if (Ship.shield > 0) {
						if (Time.time > shieldCD && shield == false) {
							if (GUI.Button (new Rect (Screen.width * 0.2f, Screen.height * 0.8f, Screen.width * 0.2f, Screen.height * 0.1f), "Shield")) {
								shieldTS = Time.time + shieldT;
								shield = true;
							}
						}
					}
				}
				if (pressed == true) {
					if (Ship.rocket > 0) {
						if (Time.time > rocketCount && rocketFire == false) {
							if (GUI.Button (new Rect (Screen.width * 0.6f, Screen.height * 0.8f, Screen.width * 0.2f, Screen.height * 0.1f), "rocket: ")) {
								Sounds.rFire = true;
								rocketCount = Time.time + rocketTime;
								rocketLoc = 0.2f;
								rocketFire = true;
							}
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
				GUI.DrawTexture (new Rect (Screen.width * rocketLocE, Screen.height * 0.5f, Screen.width * 0.048f, Screen.height * 0.1f), rocketE);
				GUI.DrawTexture (new Rect (Screen.width * location, Screen.height * 0.5f, Screen.width * 0.048f, Screen.height * 0.1f), laser);
				GUI.DrawTexture (new Rect (Screen.width * locationE, Screen.height * 0.5f, Screen.width * 0.052f, Screen.height * 0.1f), laser);
				GUI.TextField (new Rect (Screen.width * 0.8f, Screen.width * 0.02f, Screen.width * 0.1f, Screen.height * 0.05f), "Health: " + enemyHealth);

				//textbox about current point + enemy ship
				if (pressed == false && pressed2 == false) {
					if (question == false) {
						if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.52f, Screen.width * 0.05f, Screen.height * 0.05f), "ok")) {
							pressed = true;
							attack = true;
							rocketCount = Time.time + rocketTime;
							rocketCountE = Time.time + rocketTime;
							shieldTS = Time.time + shieldT;
							shieldTSE = Time.time + shieldT;
						}
					}
					if (question == true) {
						if (GUI.Button (new Rect (Screen.width * 0.52f, Screen.height * 0.52f, Screen.width * 0.05f, Screen.height * 0.05f), "yes")) {
							pressed = true;
							attack = true;
							rocketCount = Time.time + rocketTime;
							rocketCountE = Time.time + rocketTime;
							shieldTS = Time.time + shieldT;
						}
						if (GUI.Button (new Rect (Screen.width * 0.42f, Screen.height * 0.52f, Screen.width * 0.05f, Screen.height * 0.05f), "no")) {
							pressed2 = true;
						}
					}
					GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), enemyText [text]);
				}
				GUI.DrawTexture (new Rect (Screen.width * 0.65f, Screen.height * 0.4f, Screen.width * 0.3f, Screen.height * 0.3f), enemy [enemyShip]);


			}

			//When enemies health <= 0 text box appears
			if (enemyHealth <= 0) {
				if (pressed2 == false) {
					if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.52f, Screen.width * 0.05f, Screen.height * 0.05f), "ok")) {
						gameContent.scrap = gameContent.scrap + scrapGiven;
						gameContent.totalScrap = gameContent.totalScrap + scrapGiven;
						pressed2 = true;
					}
					GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), "You recieved " + scrapGiven);
				}
			}
		}
			
		if (gameContent.health <= 0) {
			gameContent.score = gameContent.totalScrap + (gameContent.health * 2) + gameContent.pointCount;
			if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * 0.5f, Screen.width * 0.1f, Screen.height * 0.05f), "Quit")) {
				Application.Quit ();
			}
			GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), "Your ship was destoryed. Your score is: " + gameContent.score);
		}

		//*************************** loads map
		if (enemyHealth < 0 || gameContent.encounterInt > 3 && pressed == true || gameContent.encounterInt > 6 && gameContent.encounterInt != 9) { 
			if (GUI.Button (new Rect (Screen.width * 0.04f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Back")) {
				SceneManager.LoadScene ("map");
				gameContent.encounterInt = 0;
				shield = false;
			}
		}
		if (pressed2 == true && gameContent.health > 0) {
			if (GUI.Button (new Rect (Screen.width * 0.04f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Back")) {
				SceneManager.LoadScene ("map");
				gameContent.encounterInt = 0;
				shield = false;
			}
		}
	}
}
