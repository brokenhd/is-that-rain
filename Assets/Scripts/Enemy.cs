using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public EnemyStats enemyStats = new EnemyStats();
	public Transform gibEffect;
	
	public void GibEnemy () {
		Transform gibParticle = Instantiate(gibEffect, transform.position, transform.rotation) as Transform;
		Destroy (gibParticle.gameObject, 2f);
	}
	
	public void DamageEnemy(int damage) {
		enemyStats.CurrentHealth -= damage;
		
		if (enemyStats.CurrentHealth <= 0) {
			GameManager.KillEnemy(this);
		}
	}
}
