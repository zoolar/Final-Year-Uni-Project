using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scrap : MonoBehaviour {
	int scrapGiven;
	bool pressed = false;

	void Start () {
		scrapGiven = Random.Range (130, 580);
	}
		
	void OnGUI () {
		if (gameContent.encounterInt == 6) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Backgrounds.background);
			if (pressed == false) {
				if (GUI.Button (new Rect (Screen.width * 0.47f, Screen.height * 0.52f, Screen.width * 0.05f, Screen.height * 0.05f), "ok")) {
					gameContent.scrap = gameContent.scrap + scrapGiven;
					gameContent.totalScrap = gameContent.totalScrap + scrapGiven;
					pressed = true;
				}
				GUI.TextArea (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f), "You found: " + scrapGiven + " scrap");
			}
		}

		if (pressed == true) {
			if (GUI.Button (new Rect (Screen.width * 0.04f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Back")) {
				SceneManager.LoadScene ("map");
				gameContent.encounterInt = 0;
			}
		}
	}
}
