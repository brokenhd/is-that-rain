using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	int lifeSpan = 1;
	
	void Start() {
		Destroy (gameObject, lifeSpan);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Floor") {
			Destroy (gameObject);
		}
	}
}
