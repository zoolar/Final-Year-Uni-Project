using UnityEngine;
using System.Collections;

public class Backgrounds : MonoBehaviour {

	public Texture[] backgroundA;
	public static Texture background;
	int i = 0;
	// Use this for initialization
	void Start () {
		i = Random.Range (0, backgroundA.Length);
		background = backgroundA [i];
	}

	void OnGUI(){
		if (gameContent.encounterInt == 10) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), background);
		}
	}
}
