﻿using UnityEngine;
using System.Collections;

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
			}
		}

		// 4 - Movement per direction
		movement = new Vector2(speed.x * inputX, speed.y * inputY);
	}

	void FixedUpdate()
	{
		// move player
		GetComponent<Rigidbody2D>().velocity = movement;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
	}

	void OnDestroy()
	{
	}
}