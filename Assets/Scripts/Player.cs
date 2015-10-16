using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public PlayerStats playerStats = new PlayerStats();

	void Update () {

		if (transform.position.y <= -2000) {
			DamagePlayer (99999);
		}

		if (Input.GetButtonDown ("Fire2")) {
			DamagePlayer (99999);
		}
	}

	public void DamagePlayer(int damage) {
		playerStats.CurrentHealth -= damage;

		if (playerStats.CurrentHealth <= 0) {
			GameManager.KillPlayer(this);
		}
	}

	public void HealPlayer(int amount) {
		playerStats.CurrentHealth += amount;
	}


}
