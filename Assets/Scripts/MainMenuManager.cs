using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public int startLives = 3; // how many lives to start the game with on New Game

	public GameObject _MainMenu;
	public GameObject _CreditsMenu;
	public GameObject _SettingsMenu;

	// references to Button GameObjects
	public GameObject MenuDefaultButton;
	public GameObject CreditsDefaultButton;
	public GameObject SettingsDefaultButton;
	public GameObject QuitButton;


	// Use this for initialization
	void Start () {
		// Show the proper menu
		ShowMenu("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
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
			//EventSystem.current.SetSelectedGameObject (MenuDefaultButton);
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
