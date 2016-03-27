using UnityEngine;
using System.Collections;
using System.IO;

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
	public AudioClip playerDestroyedSound;
	public AudioClip pickCoin;

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

		_audio.volume = 0.2F;
	}

	public void PlayExplosionSound()
	{
		PlaySound(explosionSound);
	}

	public void PlayPlayerShotSound()
	{
		PlaySound(playerShotSound);
	}

	public void PlayeEnemyShotSound()
	{
		PlaySound(enemyShotSound);
	}

	public void PlayScreamSound()
	{
		PlaySound(screamSound);
	}

	public void PlayPlayerDestroyedSound()
	{
		PlaySound (playerDestroyedSound);	
	}

	public void PlayPickCoin()
	{
		PlaySound(pickCoin);
	}
		
	public void PlayCoolSound()
	{
		PlaySound (coolSound);	
	}

	public void PlayCredits()
	{
		_audio.PlayOneShot (creditsSound);
	}

	private void PlaySound(AudioClip originalClip)
	{
		if (originalClip != null)
			_audio.PlayOneShot(originalClip);
	}
}