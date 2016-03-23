using UnityEngine;
using System.Collections;


/// <summary>
/// Projectile behavior
/// </summary>
public class ShotScript : MonoBehaviour
{
	// 1 - Designer variables

	/// <summary>
	/// Damage inflicted
	/// </summary>
	public int damage = 1;

	/// <summary>
	/// Projectile damage player or enemies?
	/// </summary>
	public bool isEnemyShot = false;

	public float destoryAfterInterval = 5;

	void Start()
	{
		// 2 - Limited time to live to avoid any leak
		Destroy(gameObject, destoryAfterInterval); // 20sec
	}
}
