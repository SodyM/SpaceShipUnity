using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	// static reference to game manager so can be called from other scripts directly (not just through gameobject component)
	public static GameManager gm;

	// levels to move to on victory and lose
	public string levelAfterVictory;
	public string levelAfterGameOver;

	// game performance
	public int score = 0;
	public int superCoolValue = 100;
	public int highscore = 0;
	public int startLives = 3;
	public int lives = 3;

	// UI elements to control
	public Text UIScore;
	public Text UILevel;

	public GameObject hudRespect;
	public GameObject hudWrong;

	GameObject _player;
	Vector3 _spawnLocation;



	// Use this for initialization
	void Start ()
	{	
		//Cursor.visible = false;

		// setup reference to game manager
		if (Instance == null)
			Instance = this.GetComponent<GameManager>();

		SetupDefaults ();
	}

	void SetupDefaults()
	{
		if (_player == null)
			_player = GameObject.FindGameObjectWithTag ("Player");

		// get initial spawn location
		_spawnLocation = _player.transform.position;

		// get stored players prefs
		RefreshPlayerState();

		// get the ui ready for the game
		RefreshGUI();
	}

	void RefreshPlayerState()
	{
		lives = PlayerPrefManager.GetLives();
		score = PlayerPrefManager.GetScore();
	}

	void RefreshGUI()
	{
		UIScore.text = "Score: " + score.ToString ();
		UILevel.text = "Level 1";
	}
		
	public void AddPoints(int amount)
	{		
		// increase score
		score += amount;

		// update UI
		UIScore.text = "Score: " + score.ToString();

		if (score % 100 == 0)
		{
			SoundEffectsHelper.Instance.PlayCoolSound ();
			StartCoroutine (ShowCoolInfo ());
		}
	}

	IEnumerator ShowCoolInfo()
	{
		hudRespect.SetActive (true);	
		yield return new WaitForSeconds (2);
		hudRespect.SetActive (false);	
	}

	public void ShowDamage()
	{
		StartCoroutine (DisplayDamageInfo ());
	}

	IEnumerator DisplayDamageInfo()
	{
		hudWrong.SetActive (true);	
		yield return new WaitForSeconds (1.5f);
		hudWrong.SetActive (false);	
	}

	public void ResetGame()
	{
		lives--;
		RefreshGUI();
		if (lives <= 0)
		{
			// no more lives, save current player prefs before going to game over
			PlayerPrefManager.SavePlayerState (score, lives);

			// load game over screen
			//Application.LoadLevel ()

		}
		else {
			// respawn player
			//_player.GetComponent<Player>().Respawn(_spawnLocation);
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
}