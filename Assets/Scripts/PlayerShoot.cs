using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public GameObject projectile;
	public float distance = 10.0f;
	public float speed = 10.0f;

	void Update() {
		if(Input.GetMouseButtonDown (0)) {
			Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			direction.Normalize();
			GameObject shot = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
			shot.GetComponent<Rigidbody2D>().velocity = direction * speed;
		}
	}
}
