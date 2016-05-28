using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;

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

    public static int GetHighscore()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            return PlayerPrefs.GetInt("Highscore");
        } else
        {
            return 0;
        }
    }

    public static void SetHighscore(int highscore)
    {
        PlayerPrefs.SetInt("Highscore",highscore);
    }

    // reset stored player state and variables back to defaults
    public static void ResetPlayerState(int startLives, bool resetHighscore)
    {
        PlayerPrefs.SetInt("Lives", startLives);
        PlayerPrefs.SetInt("Score", 0);

        if (resetHighscore)
            PlayerPrefs.SetInt("Highscore", 0);
    }

	
    // story the current player state info into PlayerPrefs
    public static void SavePlayerState(int score, int highScore, int lives)
    {
        // save currentscore and lives to PlayerPrefs for moving to next level
        PlayerPrefs.SetInt("Score",score);
        PlayerPrefs.SetInt("Lives",lives);
        PlayerPrefs.SetInt("Highscore",highScore);
    }

    // store a key for the name of the current level to indicate it is unlocked
    public static void UnlockLevel()
    {  
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name,1);
    }
}
