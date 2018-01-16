using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class Seed : MonoBehaviour {
	public static int seed;
	public int seeda;


	//private float[] noiseValues;

	void Start () {
		seed = seeda;
		if (seed == 0) {
			seed = Random.Range (0, 99999);
			Random.seed = seed;
		} else {
			Random.seed = 2;
		}

		DontDestroyOnLoad (gameObject);
	}
	void OnGUI (){
		GUI.TextArea (new Rect (Screen.width * 0.9f ,  Screen.height * 0.00f, Screen.width *0.07f, Screen.height * 0.05f), "Seed: " + seed);
	}
}
	