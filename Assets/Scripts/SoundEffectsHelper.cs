using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{
	AudioSource 	_audio;

	/// <summary>
	/// Singleton
	/// </summary>
	public static SoundEffectsHelper Instance;

	public AudioClip explosionSound;
	public AudioClip playerShotSound;
	public AudioClip enemyShotSound;

	public AudioClip clickSound;
	public AudioClip coolSound;
	public AudioClip screamSound;
	public AudioClip creditsSound;
	public AudioClip heySound;

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;

		_audio = GetComponent<AudioSource>();
		if (_audio == null)
		{ 
			// if AudioSource is missing
			//Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
			// let's just add the AudioSource component dynamically
			_audio = gameObject.AddComponent<AudioSource>();
		}
	}

	public void MakeExplosionSound()
	{
		MakeSound(explosionSound);
	}

	public void MakePlayerShotSound()
	{
		_audio.volume = 1;
		MakeSound(playerShotSound);
	}

	public void MakeEnemyShotSound()
	{
		MakeSound(enemyShotSound);
	}

	private void MakeSound(AudioClip originalClip)
	{
		_audio.PlayOneShot(originalClip);
	}
}