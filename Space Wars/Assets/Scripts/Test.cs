using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public Texture current;

	static Vector2[] loc = new Vector2[25];   
	float width = 0.0f;
	bool[] visited = new bool[25];

	bool[] selected = new bool[25];
	int selecteda = 0;

	public Texture tex;
	public Texture texb;
	Vector3 pos1 = new Vector3 (Screen.width * 0.1f, Screen.height * 0.1f, -1.0f);
	Vector3 pos2 = new Vector3 (Screen.width * 0.9f, Screen.height * 0.9f, 1.0f);


	void Start(){
		int i = 0;
		while (i < loc.Length) {
			loc [i] = new Vector2 (Random.Range (Screen.width * width, Screen.width * (width + 0.04f)), Random.Range (Screen.height * 0.1f, Screen.height * 0.9f));
			selected [i] = false;
			selected [0] = true;
			width = width + 0.039f;
			//print (loc [i]);
			i++;

		}

	}

	// Update is called once per frame
	void OnGUI () {
		




		if (Vector2.Distance (loc [0], loc [1]) < 250) {
			//print("thisthateh");
		}


		
		for (int i = 0; i < loc.Length; i++) {
			GUI.DrawTexture (new Rect (loc [i].x, loc [i].y, Screen.width * 0.025f, Screen.height * 0.05f), visited [i] ? texb : tex);
			 


			if (Vector2.Distance (loc [selecteda], loc [i]) < Screen.width * 0.2f) {
				
				GUI.DrawTexture (new Rect (loc [i].x, loc [i].y, Screen.width * 0.025f, Screen.height * 0.05f), current);
				GUI.TextArea(new Rect(loc [selecteda].x - (Screen.height * 0.01f), loc [selecteda].y - (Screen.height * 0.04f), Screen.width * 0.025f, Screen.height * 0.05f), "Current", "");

				if (GUI.Button (new Rect (loc [i].x, loc [i].y, Screen.width * 0.025f, Screen.height * 0.05f), visited [i] ? texb : tex, "")) {

					print ("clicked");

					//print ("pre:" + selected [i]);

					selected [selecteda] = false;
					selected [i] = true;
					selecteda = i;



					//print ("post: " + selected [i]);
				}
			}
		}
		//printt = health * 100;
		//print (printt);
	}
}
