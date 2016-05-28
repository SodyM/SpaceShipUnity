using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;


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
    public Text UIHighScore;
	public Text UILevel;
    public GameObject[] UIExtraLives;
    public GameObject UIGamePaused;

	public GameObject hudRespect;
	public GameObject hudWrong;

	GameObject _player;
	Vector3 _spawnLocation;


    // set things up here
    void Awake () {

        // setup reference to game manager
        if (gm == null)
            gm = this.GetComponent<GameManager>();

        if (Instance == null)
            Instance = this.GetComponent<GameManager>();
        
        // setup all the variables, the UI, and provide errors if things not setup properly.
        SetupDefaults();
    }

    /*
	// Use this for initialization
	void Start ()
	{	
		//Cursor.visible = false;

		// setup reference to game manager
		if (Instance == null)
			Instance = this.GetComponent<GameManager>();

		SetupDefaults ();
	}
    */

    // game loop
    void Update() {        
        if (Input.GetKeyDown(KeyCode.Escape))                   // if ESC pressed then pause the game
        {
            if (Time.timeScale > 0f)
            {
                UIGamePaused.SetActive(true);                   // this brings up the pause UI
                Time.timeScale = 0f;                            // this pauses the game action
            } else
            {
                Time.timeScale = 1f;                            // this unpauses the game action (ie. back to normal)
                UIGamePaused.SetActive(false);                  // remove the pause UI
            }
        }
    }

	void SetupDefaults()
	{
		if (_player == null)
			_player = GameObject.FindGameObjectWithTag ("Player");

		// get initial spawn location
		_spawnLocation = _player.transform.position;

        // if levels not specified, default to current level
        if (levelAfterVictory=="") {
            Debug.LogWarning("levelAfterVictory not specified, defaulted to current level");
            levelAfterVictory = SceneManager.GetActiveScene().name;
        }

        if (levelAfterGameOver=="") {
            Debug.LogWarning("levelAfterGameOver not specified, defaulted to current level");
            levelAfterGameOver = SceneManager.GetActiveScene().name;
        }

        // friendly error messages
        if (UIScore==null)
            Debug.LogError ("Need to set UIScore on Game Manager.");

        if (UILevel==null)
            Debug.LogError ("Need to set UILevel on Game Manager.");

        if (UIGamePaused==null)
            Debug.LogError ("Need to set UIGamePaused on Game Manager.");
        
        PlayerPrefManager.SetLives(lives);

		// get stored players prefs
		RefreshPlayerState();

		// get the ui ready for the game
		refreshGUI();
	}



	void RefreshPlayerState()
	{   
        lives = PlayerPrefManager.GetLives();

        // special case if lives <= 0 then must be testing in editor, so reset the player prefs
        if (lives <= 0) {
            PlayerPrefManager.ResetPlayerState(startLives,false);
            lives = PlayerPrefManager.GetLives();
        }
        score = PlayerPrefManager.GetScore();
        highscore = PlayerPrefManager.GetHighscore();

        // save that this level has been accessed so the MainMenu can enable it
        PlayerPrefManager.UnlockLevel();
	}

    // refresh all the GUI elements
    void refreshGUI() {
        // set the text elements of the UI
        UIScore.text = "Score: "+score.ToString();
        //UIHighScore.text = "Highscore: "+highscore.ToString ();
        UILevel.text = SceneManager.GetActiveScene().name;

        // turn on the appropriate number of life indicators in the UI based on the number of lives left
        for(int i=0;i<UIExtraLives.Length;i++) {
            if (i<(lives-1)) { // show one less than the number of lives since you only typically show lifes after the current life in UI
                UIExtraLives[i].SetActive(true);
            } else {
                UIExtraLives[i].SetActive(false);
            }
        }
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

    // public function to remove player life and reset game accordingly
    public void ResetGame() {
        // remove life and update GUI
        lives--;
        refreshGUI();

        if (lives<=0) { // no more lives            
            PlayerPrefManager.SavePlayerState(score, highscore, lives);                 // save the current player prefs before going to GameOver
            SceneManager.LoadScene(levelAfterGameOver);                                 // load the gameOver screen
        }
        else
        { 
            // tell the player to respawn
            //_player.GetComponent<CharacterController2D>().Respawn(_spawnLocation);
            SceneManager.LoadScene("Level1");                                 // load the gameOver screen
        }
    }

    // public function for level complete
    public void LevelCompete()
    {        
        PlayerPrefManager.SavePlayerState(score,highscore,lives);                   // save the current player prefs before moving to the next level
        StartCoroutine(LoadNextLevel());                                            // use a coroutine to allow the player to get fanfare before moving to next level
    }

    // load the nextLevel after delay
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1.5f); 
        SceneManager.LoadScene(levelAfterVictory);
    }
}