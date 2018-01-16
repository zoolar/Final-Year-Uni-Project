using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {


	/* ship script includes:
	 * ship selection - updates stats depending on which ship is selected
	 * public static int (stats) to pass through to encounter so ships stats can be upgraded at shop
	 * public static int (stats) are used in encounter to determin deffence, attack and health
	 * 
	 */
	bool selection = true;
	public Texture background;
	public Texture shieldTex;
	//********** Ship selection
	public Texture[] ship;
	int selected = 0;
	//********** Ship selection
	//******** ship stats
	static Vector2[] engineStat = new Vector2[10];
	static Vector2[] weaponStat = new Vector2[10];
	static Vector2[] healthStat = new Vector2[10];
	static Vector2[] shieldStat = new Vector2[3];
	static Vector2[] rocketStat = new Vector2[3];
	float statGap = 0.62f;
	float statGap2 = 0.81f;
	public Texture stat;
	public Texture statb;
	bool[] statsE = new bool[10];
	bool[] statsW = new bool[10];
	bool[] statsH = new bool[10];
	bool[] statsS = new bool[3];
	bool[] statsR = new bool[3];
	int[] statCount = new int[5];
	public static int engine;
	public static int weapon;
	public static int health;
	public static int shield = 0;
	public static int rocket = 0;

	//************ship stats


	// Use this for initialization
	void Start () {
		// sets locations of stats into a vector 2 array
		for (int i = 0; i < engineStat.Length; i++) {
			engineStat [i] = new Vector2 (Screen.width * statGap, Screen.height * 0.47f);
			weaponStat [i] = new Vector2 (Screen.width * statGap, Screen.height * 0.57f);
			healthStat [i] = new Vector2 (Screen.width * statGap, Screen.height * 0.67f);
			statGap = statGap + 0.031f;
		}

		for (int i = 0; i < shieldStat.Length; i++) {
			shieldStat [i] = new Vector2 (Screen.width * statGap2, Screen.height * 0.15f);
			rocketStat [i] = new Vector2 (Screen.width * statGap2, Screen.height * 0.31f);
			statGap2 = statGap2 + 0.038f;
		}
		//********** ship stats

		DontDestroyOnLoad (gameObject);
	}
	void Update(){
		// if the change ship button is pressed then update changes the variables to match the selected ship
		if (selection == true) {
			if (selected == 0) {
				engine = 3;
				weapon = 5;
				health  = 4;
			}
			if (selected == 1) {
				engine = 6;
				weapon = 3;
				health  = 3;
			}
		}
	}
		
	int e = 0;
	int w = 0;
	int a = 0;
	int s = 0;
	int r = 0;
	void OnGUI (){
		gameContent.level = Application.loadedLevelName;

		if (gameContent.level == "gameOptions") {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), background);
		}

		if (gameContent.level == "encounter" && gameContent.encounterInt == 7) {


			for (int i = 0; i < shieldStat.Length; i++) {
				GUI.DrawTexture (new Rect (shieldStat [i].x, shieldStat [i].y, Screen.width * 0.03f, Screen.height * 0.04f), statsS [i] ? statb : stat);
			

				if (shield <= 3) {
					if (s < shield) {
						statsS [statCount [3]] = !statsS [statCount [3]];
						statCount [3]++;
						s++;
					}
				}
			}
			for (int i = 0; i < rocketStat.Length; i++) {
				GUI.DrawTexture (new Rect (rocketStat [i].x, rocketStat [i].y, Screen.width * 0.03f, Screen.height * 0.04f), statsR [i] ? statb : stat);
				if (rocket <= 3) {
					if (r < rocket) {
						statsR [statCount [4]] = !statsR [statCount [4]];
						statCount [4]++;
						r++;
					}
				}
			}
		}
			
		if (gameContent.level == "gameOptions" || gameContent.encounterInt == 7) {
			
			//ship stats
			// ship selection screen stats
			for (int i = 0; i < engineStat.Length; i++) {
				GUI.DrawTexture (new Rect (engineStat [i].x, engineStat [i].y, Screen.width * 0.03f, Screen.height * 0.04f), statsE [i] ? statb : stat);
				if (engine <= 10) {
					if (e < engine) {
						statsE [statCount [0]] = !statsE [statCount [0]]; //switch texture
						statCount [0]++; //texture to swtich + 1
						e++;
					}
					if (e > engine) {
						statCount [0]--;
						statsE [statCount [0]] = !statsE [statCount [0]]; //switch texture
						e--;
					}
				}
			}
			for (int i = 0; i < weaponStat.Length; i++) {
				GUI.DrawTexture (new Rect (weaponStat [i].x, weaponStat [i].y, Screen.width * 0.03f, Screen.height * 0.04f), statsW [i] ? statb : stat);
				if (weapon <= 10) {
					if (w < weapon) {
						statsW [statCount [1]] = !statsW [statCount [1]]; //switch texture
						statCount [1]++; //texture to swtich + 1
						w++;
					}
					if (w > weapon) {
						statCount [1]--;
						statsW [statCount [1]] = !statsW [statCount [1]]; //switch texture
						w--;
					}
				}
			}
			for (int i = 0; i < healthStat.Length; i++) {
				GUI.DrawTexture (new Rect (healthStat [i].x, healthStat [i].y, Screen.width * 0.03f, Screen.height * 0.04f), statsH [i] ? statb : stat);
				if (health <= 10) {	
					if (a < health) {
						statsH [statCount [2]] = !statsH [statCount [2]]; //switch texture
						statCount [2]++; //texture to swtich + 1
						a++;
					}
					if (a > health) {
						statCount [2]--;
						statsH [statCount [2]] = !statsH [statCount [2]]; //switch texture
						a--;
					}
				}
			}
		}
		if (gameContent.level == "gameOptions") {
			if (GUI.Button (new Rect (Screen.width * 0.5f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Start Game")) {
				// if true changes stats (ship script)
				selection = false;
				gameContent.maxHealth = gameContent.maxHealth + (50 * Ship.health);
				gameContent.health = gameContent.health + (50 * Ship.health);
				gameOptions.change = true;
			}
		}



		//************** Ship Slection
		if (gameContent.level != "map") {
			GUI.DrawTexture (new Rect (Screen.width * 0.072f, Screen.height * 0.4f, Screen.width * 0.3f, Screen.height * 0.3f), ship [selected]);
			if (gameContent.level != "encounter") {
				if (GUI.Button (new Rect (Screen.width * 0.05f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Change Ship")) {
					selected++;
					if (selected == ship.Length) {
						selected = 0;
					} 
				}
			}
		}
	}
}