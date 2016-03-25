using UnityEngine;
using System.Collections;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
	/// <summary>
	/// Total hitpoints
	/// </summary>
	public int hp = 1;

	public int damagePoints = 1;

	/// <summary>
	/// Enemy or player?
	/// </summary>
	public bool isEnemy = true;

	/// <summary>
	/// Inflicts damage and check if the object should be destroyed
	/// </summary>
	/// <param name="damageCount"></param>
	public void Damage(int damageCount)
	{
		Debug.Log ("damageCount: " + damageCount);

		hp -= damageCount;

		if (isEnemy)
		{
			if (hp <= 0)
			{	
				// add explostion animation object
				SpecialEffectsHelper.Instance.AddExplosion(transform.position);

				// Play explosion sound effect
				SoundEffectsHelper.Instance.PlayExplosionSound();

				// Dead!
				Destroy(gameObject);
			}	
		}
		else{
			if (hp <= 0)
			{
				// add explostion animation object
				SpecialEffectsHelper.Instance.AddExplosion(transform.position);

				// Play explosion sound effect
				SoundEffectsHelper.Instance.PlayExplosionSound();

				SoundEffectsHelper.Instance.PlayPlayerDestroyedSound();


				// Dead!
				Destroy(gameObject);
			}
			else
			{
				SoundEffectsHelper.Instance.PlayScreamSound();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)
			{
				Damage(shot.damage);

				// Destroy the shot
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
			}
		}
	}
}