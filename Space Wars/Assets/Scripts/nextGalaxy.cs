using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class nextGalaxy : MonoBehaviour {
	void OnGUI(){
		if (gameContent.encounterInt == 8) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Backgrounds.background);
			if (GUI.Button (new Rect (Screen.width * 0.5f, Screen.height * 0.5f, Screen.width * 0.2f, Screen.height * 0.2f), "Next Galaxy")) {
				gameContent.nextGalaxy = true;
				SceneManager.LoadScene ("map");
			}
		}
	}
}
