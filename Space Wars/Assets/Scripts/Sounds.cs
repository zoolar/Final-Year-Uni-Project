using UnityEngine;
using System.Collections;


public class Sounds : MonoBehaviour {
	public AudioClip[] sound;
	AudioSource audio;
	public static bool isPlaying;
	public static bool fire;
	public static bool hit;
	public static bool rFire;
	public static bool rHit;
	bool selection = true;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		isPlaying = false;
		fire = false;
		hit = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameContent.encounterInt >= 0 && gameContent.encounterInt <= 3 || gameContent.encounterInt == 10) {
			if (isPlaying == false) {
				isPlaying = true;
				audio.PlayOneShot (sound [0], 0.5f);
			}
		}
		if (gameContent.encounterInt == 4 || gameContent.encounterInt == 5) {
			if (isPlaying == false) {
				isPlaying = true;
				audio.PlayOneShot (sound [1], 1.0f);
			}
		}
		if (gameContent.encounterInt == 6) {
			if (isPlaying == false) {
				isPlaying = true;
				audio.PlayOneShot (sound [2], 1.0f);
			}
		}
		if (gameContent.encounterInt == 7) {
			if (isPlaying == false) {
				isPlaying = true;
				audio.PlayOneShot (sound [3], 1.0f);
			}
		}
		if (gameContent.encounterInt == 8) {
			if (isPlaying == false) {
				isPlaying = true;
				audio.PlayOneShot (sound [2], 1.0f);
			}
		}
		if (gameContent.encounterInt == 8) {
			if (isPlaying == false) {
				isPlaying = true;
				audio.PlayOneShot (sound [0], 1.0f);
			}
		}
		if (fire == true) {
			audio.PlayOneShot (sound [4], 1.0f);
			fire = false;
		}
		if (hit == true) {
			audio.PlayOneShot (sound [5], 1.0f);
			hit = false;
		}
		if (rFire == true) {
			print (rHit);
			audio.PlayOneShot (sound [6], 1.0f);
			rFire = false;
		}
		if (rHit == true) {
			audio.PlayOneShot (sound [7], 1.0f);
			rHit = false;
		}
	}
}
