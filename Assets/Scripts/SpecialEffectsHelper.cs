using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

/// <summary>
/// Creating instance of particles from code with no effort
/// </summary>
public class SpecialEffectsHelper : MonoBehaviour
{
	/// <summary>
	/// Singleton
	/// </summary>
	public static SpecialEffectsHelper Instance;

	public Transform explosion;

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SpecialEffectsHelper!");
		}

		Instance = this;
	}

	public void AddExplosion(Vector3 position)
	{		
		Transform explosionObject = (Transform)Instantiate(explosion, position, transform.rotation);
	}
}
