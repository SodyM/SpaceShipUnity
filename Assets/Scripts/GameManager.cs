using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	// static reference to game manager so can be called from other scripts directly (not just through gameobject component)
	//public static GameManager gm;

	public int superCoolValue = 100;
	public int score = 0;

	// UI elements to control
	public Text UIScore;

	public GameObject hudRespect;
	public GameObject hudWrong;

	// Use this for initialization
	void Start ()
	{	
		Cursor.visible = false;

		// setup reference to game manager
		if (Instance == null)
			Instance = this.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{	
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
}