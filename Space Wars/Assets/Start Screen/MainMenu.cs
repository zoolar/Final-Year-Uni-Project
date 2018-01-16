using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

	public Texture background;
	public Texture playGame;
	public Texture instructions;
	public Texture text;
	public Texture quit;
	bool pressed = false;

	private void OnGUI(){
		//Display backgroun texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), background);

		//Display Button
		if (GUI.Button (new Rect (Screen.width * 0.05f, Screen.height * 0.4f, Screen.width * 0.25f, Screen.height * 0.1f), playGame, "")) 
		{
			SceneManager.LoadScene("gameOptions");
		}
		if (GUI.Button (new Rect (Screen.width * 0.05f, Screen.height * 0.55f, Screen.width * 0.25f, Screen.height * 0.1f), instructions, "")) 
		{
			pressed = true;
		}
		if (GUI.Button (new Rect (Screen.width * 0.05f, Screen.height * 0.7f, Screen.width * 0.25f, Screen.height * 0.1f), quit, "")) 
		{
			Application.Quit ();
		}
		if (pressed == true) {
			GUI.DrawTexture (new Rect (Screen.width * 0.3f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.8f), text);
		}
	}
}
