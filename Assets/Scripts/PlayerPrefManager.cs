using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public static class PlayerPrefManager
{
	public static int GetLives()
	{
		if (PlayerPrefs.HasKey ("Lives"))
		{
			return PlayerPrefs.GetInt ("Lives");
		}
		return 0;
	}

	public static void SetLives(int lives)
	{
		PlayerPrefs.SetInt ("Lives", lives);
	}

	public static int GetScore()
	{
		if (PlayerPrefs.HasKey ("Score"))
		{
			return PlayerPrefs.GetInt ("Score");
		}
		return 0;
	}

	public static void SetScore(int score)
	{
		PlayerPrefs.SetInt ("Score", score);
	}

	public static void SavePlayerState(int score, int lives)
	{
		// save current data to PlayerPrefs for moving to next level
		SetLives (lives);
		SetScore (score);
	}
}
