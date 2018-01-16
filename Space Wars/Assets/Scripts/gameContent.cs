using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameContent : MonoBehaviour {
	public Texture background;
	public Texture background1;
	static Vector2[] loc = new Vector2[25];   		//vector2 array coordinates
	static Vector2[] loc2 = new Vector2[25];
	int[] encounterType = new int[25];				//Array of encounter types	
	int[] encounterType2 = new int[25];	
	public static string level;	//level is used to set the level name so it can be checked on different levels
	float width = 0.0f;
	public Texture current;
	bool[] selected = new bool[25];
	int selectedNode = 0;
	public static int score;
	public static int totalScrap;
	void Start()
	{
		Random.seed = Seed.seed;
		//Randomly Places points on a map
		int i = 0;
		while (i < loc.Length) {
			loc [i] = new Vector2 (Random.Range (Screen.width * width, Screen.width * (width + 0.04f)), Random.Range (Screen.height * 0.1f, Screen.height * 0.9f));
			loc2 [i] = new Vector2 (Random.Range (Screen.width * width, Screen.width * (width + 0.04f)), Random.Range (Screen.height * 0.1f, Screen.height * 0.9f));
			selected [i] = false;
			selected [0] = true;
			width = width + 0.039f;
			i++;
		}

		//fills arry with encounter type, 1-3 attack, 4-5 random dialog, 6 random item,
		for (int y = 0; y < encounterType.Length; y++) 
		{
			encounterType [y] = Random.Range (1, 7);
			encounterType [7] = 7;
			encounterType [23] = 7;
			encounterType [24] = 8;
			encounterType2 [y] = Random.Range (1, 7);
			encounterType2 [7] = 7;
			encounterType2 [23] = 7;
			encounterType2 [24] = 9;
		}
			
		DontDestroyOnLoad (gameObject);
	}
	//*********
	public static bool nextGalaxy = false;

	void Update (){
		level = Application.loadedLevelName;
		if (nextGalaxy == true){
			for (int i = 0; i < loc.Length; i++){
				loc [i] = loc2 [i];
				encounterType [i] = encounterType2 [i];
				selected [i] = false;
				selected [0] = true;
				visited [i] = false;
				background = background1;
			}
			selectedNode = 0;
			enemyFleet = -0.1f;
			nextGalaxy = false;
		}

	}


	// resources + health
	public static int scrap = 1500;
	public static int maxHealth = 2000;
	public static int health = 2000;


	//********** Map nodes	
	public Texture tex;
	public Texture texb;
	public static int encounterInt;
	bool[] visited = new bool[25];
	public static int pointCount = 0;

	public static bool playSound = true;


	public static bool enemyLine = false;
	float enemyFleet = -0.1f;
	void OnGUI (){
		// when level = map, show nodes at set locations
		if (level == "map") {
			{
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), background);


				if (encounterType [24] == 9) {
					GUI.TextArea (new Rect (loc [24].x, loc [24].y - 20.0f, Screen.width * 0.025f, Screen.height * 0.05f), "Boss", "");
				}
				for (int i = 0; i < loc.Length; i++) {
					GUI.DrawTexture (new Rect (loc [i].x, loc [i].y, Screen.width * 0.025f, Screen.height * 0.05f), visited [i] ? texb : tex);
					if (Vector2.Distance (loc [selectedNode], loc [i]) < Screen.width * 0.25f) {
						GUI.DrawTexture (new Rect (loc [i].x, loc [i].y, Screen.width * 0.025f, Screen.height * 0.05f), current);
						GUI.TextArea (new Rect (loc [selectedNode].x - (Screen.height * 0.01f), loc [selectedNode].y - (Screen.height * 0.05f), Screen.width * 0.025f, Screen.height * 0.05f), "Current", "");

						if (GUI.Button (new Rect (loc [i].x, loc [i].y, Screen.width * 0.025f, Screen.height * 0.05f), visited [i] ? texb : tex, "")) {

							//changes current node
							pointCount++;
							selected [selectedNode] = false;
							selected [i] = true;
							selectedNode = i;
							// visited nodes change texture
							if (visited [i] == false) {
								visited [i] = !visited [i];
							}
							encounterInt = encounterType [i];

							// if encounter is behind enemy fleet line (attacked)
							if (loc [i].x < Screen.width * enemyFleet) {
								if (encounterInt != 8 && encounterInt != 9) {
									encounterInt = 1;
									enemyLine = true;
								}
							} else {
								enemyLine = false;
							}
							enemyFleet = enemyFleet + 0.05f;
							SceneManager.LoadScene ("encounter");
							if (encounterInt < 7) { 
								encounterType [i] = 10;
							}
						}
					}
				}
				GUI.TextArea(new Rect(Screen.width * - 0.1f, 0.0f, Screen.width * (enemyFleet + 0.1f), Screen.height), "");
			}
			GUI.TextArea (new Rect (loc [7].x, loc [7].y - 20.0f, Screen.width * 0.025f, Screen.height * 0.05f), "shop", "");
			GUI.TextArea (new Rect (loc [23].x, loc [23].y - 20.0f, Screen.width * 0.025f, Screen.height * 0.03f), "shop", "");
		}
			// Scrap + Health bars
		if (level == "encounter") {
			GUI.TextField (new Rect (Screen.width * 0.05f, Screen.width * 0.02f, Screen.width * 0.1f, Screen.height * 0.05f), "Health: " + health);
			GUI.TextField (new Rect (Screen.width * 0.05f, Screen.width * 0.05f, Screen.width * 0.1f, Screen.height * 0.05f), "Scrap: " + scrap);
		}
	}
}