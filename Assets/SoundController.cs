using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public AudioClip BackgroundMusic;

	// Use this for initialization
	void Start () {
        MusicController.PlayMusic(BackgroundMusic);
	}	
}
