using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	/// 1 - The speed of the ship
	public Vector2 speed = new Vector2(50, 50);

	// 2 - Store the movement
	private Vector2 movement;

	void Update()
	{
		// 3 - Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		bool spaceUp = Input.GetKeyDown(KeyCode.Space);
		if (spaceUp)
		{
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				weapon.Attack (false);	
				SoundEffectsHelper.Instance.PlayPlayerShotSound ();
			}
		}

		// 4 - Movement per direction
		movement = new Vector2(speed.x * inputX, speed.y * inputY);

		// 6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
		).x + 1;

		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
		).x - 1;

		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
		).y + 1;

		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
		).y - 1;

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, bottomBorder, topBorder),
			transform.position.z
		);
	}

	void FixedUpdate()
	{
		// move player
		GetComponent<Rigidbody2D>().velocity = movement;
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		bool damagePlayer = false;

		if (otherCollider.gameObject.tag.Equals ("CreditsHelper"))
		{
			// stop scrolling

			// stop main music
			Camera.main.GetComponent<AudioSource>().Stop ();

			// play loop
			SoundEffectsHelper.Instance.PlayCredits ();

			gameObject.SetActive (false);
		}

		// Collision with enemy
		EnemyScript enemy = otherCollider.gameObject.GetComponent<EnemyScript>();
		if (enemy != null)
		{
			// Kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if (enemyHealth != null)
			{
				enemyHealth.Damage(enemyHealth.hp);
				//GameManager.Instance.AddPoints (10);
			}
				
			damagePlayer = true;
		}

		CoinScript coin = otherCollider.gameObject.GetComponent<CoinScript>();
		if (coin != null)
		{
			GameManager.Instance.AddPoints (10);
			SoundEffectsHelper.Instance.PlayPickCoin();
			coin.DestroyCoin();
		}


        if (otherCollider.gameObject.tag.Equals("EndLevel"))
        {
            if (GameManager.gm) // do the game manager level compete stuff, if it is available
                GameManager.gm.LevelCompete();
        }

		// Damage the player
		if (damagePlayer)
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null)
            {
                playerHealth.Damage(otherCollider.gameObject.GetComponent<HealthScript>().damagePoints);
                if (playerHealth.hp <= 0)
                {
                    KillPlayer();
                }
            }				            
		}
	}
		
	void OnDestroy()
	{
	}

	public void KillPlayer()
	{	        
        if (GameManager.gm) // if the gameManager is available, tell it to reset the game
            GameManager.gm.ResetGame();
        else // otherwise, just reload the current level
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);       
	}
}