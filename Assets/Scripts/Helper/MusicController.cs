using UnityEngine;

public class MusicController : MonoBehaviour {

	private static bool _silent;
	private static float _volume = 1f;

	private static GameObject _currentMusic;

	public static void PlaySingle(AudioClip clip){
		if (_silent) return;

		GameObject obj = CreateAudioSource2D (clip);
		Destroy (obj, clip.length);
	}

	public static void Toggle(bool toggled){
		_silent = !toggled;

		if (_currentMusic != null) {
			if (_silent){
				_currentMusic.GetComponent<AudioSource>().Pause();
			} else {
				_currentMusic.GetComponent<AudioSource>().UnPause();
			}
		}
	}

	public static void SetVolume (float volume)
	{
		_volume = volume;
		
		if (_currentMusic != null) {
			_currentMusic.GetComponent<AudioSource> ().volume = _volume;
		}
	}

	public static void PlayMusic(AudioClip clip){
		if (_currentMusic == null) {
			_currentMusic = CreateAudioSource2D (clip, true);
            _currentMusic.name = "BackgroundMusic";
		} else {
			var audioSource = _currentMusic.GetComponent<AudioSource>();
			audioSource.Stop();
			audioSource.clip = clip;
			if(!_silent) audioSource.Play();
		}
	}

	static GameObject CreateAudioSource2D (AudioClip clip, bool loop = false)
	{
		GameObject obj = new GameObject ();
		obj.transform.position = Vector3.zero;
		obj.AddComponent<AudioSource> ();
		DontDestroyOnLoad (obj);

		var audioSource = obj.GetComponent<AudioSource> ();
		audioSource.volume = _volume;
		audioSource.clip = clip;
		audioSource.loop = loop;
		if(!_silent) audioSource.Play();       
		return obj;
	}
}
