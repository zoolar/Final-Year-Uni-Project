using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class gameOptions : MonoBehaviour {
	public static bool change = false;
	void OnGUI()
	{
		if (change == true) 
		{
			SceneManager.LoadScene ("map");
		}
	}
}
