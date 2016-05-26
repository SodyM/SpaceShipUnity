using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	AudioSource 	_audio;

	public int startLives = 3; // how many lives to start the game with on New Game

	public GameObject _MainMenu;
	public GameObject _CreditsMenu;
	public GameObject _SettingsMenu;

	// references to Button GameObjects
	public GameObject MenuDefaultButton;
	public GameObject CreditsDefaultButton;
	public GameObject SettingsDefaultButton;
	public GameObject QuitButton;

	public AudioClip clickSound;

	private Animator[] _animators;

    private string _actualView = "Mainmenu";

	int activeIndex = 0;

	// Use this for initialization
	void Start ()
    {
		_audio = GetComponent<AudioSource>();
		if (_audio == null)
		{ 
			// if AudioSource is missing
			//Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
			// let's just add the AudioSource component dynamically
			_audio = gameObject.AddComponent<AudioSource>();
		}

		// save animators
		_animators = _MainMenu.GetComponentsInChildren<Animator> ();

		// Show the proper menu
		ShowMenu("MainMenu");
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool up = Input.GetKeyDown(KeyCode.UpArrow);
		if (up){
			if (activeIndex > 0)
				activeIndex--;
			else
				activeIndex = 0;

			SetMenuAsActive (activeIndex);
		}

		bool down = Input.GetKeyDown(KeyCode.DownArrow);
		if (down){
			if (activeIndex < 2)
				activeIndex++;
			else
				activeIndex = 0;

			SetMenuAsActive(activeIndex);
		}

        if (Input.GetKeyDown(KeyCode.Return))
            ProcessEnter();
	}

    void ProcessEnter()
    {        
        Debug.Log("_actualView: " + _actualView);
        if (_actualView == "Mainmenu")
        {
            if (activeIndex == 0)
            {
                _actualView = "Level1";
                SceneManager.LoadScene("Level1");
            }
            else if (activeIndex == 1)
            {
                _actualView = "Credits";
                SceneManager.LoadScene("Credits");
            }
            else
            {   
                Application.Quit();
            }   
        }
        else if(_actualView == "Credits")
        {
            if (activeIndex == 0)
            {
                _actualView = "Mainmenu";
                SceneManager.LoadScene("Mainmenu");
            }
        }
    }

	void SetMenuAsActive(int index)
	{
		// deactivate all animators
		foreach(var animator in _animators)
		{
			animator.ResetTrigger ("Selected");	
		}

		_animators[index].SetTrigger("Selected");

		_audio.PlayOneShot(clickSound);
	}

	// Show the proper menu
	public void ShowMenu(string name)
	{
		// turn all menus off
		_MainMenu.SetActive (false);
		//_AboutMenu.SetActive(false);
		//_LevelsMenu.SetActive(false);

		// turn on desired menu and set default selected button for controller input
		switch(name) {
		case "MainMenu":
			_MainMenu.SetActive (true);
			SetMenuAsActive(0);
			break;
		case "LevelSelect":
			//_LevelsMenu.SetActive(true);
			//EventSystem.current.SetSelectedGameObject (LevelSelectDefaultButton);
			break;
		case "About":
			//_AboutMenu.SetActive(true);
			//EventSystem.current.SetSelectedGameObject (AboutDefaultButton);
			break;
		}
	}
}