using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	public Transform BulletTracerPrefab;
	public float effectSpawnRate = 10;
	public Transform MuzzleFlashPrefab;

	float timeToFire = 0;
	Transform firePoint;
	float timeToSpawnEffect = 0;
	MoveTracer moveTracer;

	void Awake () {
		moveTracer = new MoveTracer();


		firePoint = transform.FindChild("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No FirePoint was set on the gun");
		}
	}

	void Update () {
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot();
			}
		} else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = (Time.time + 1) / fireRate;
				Shoot();
			}
		}
	}

	void Shoot () {
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

		if (Time.time >= timeToSpawnEffect) {
			Effect();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}

		if (hit.collider != null) {
			//if(hit.collider.name == "Floor") moveTracer.KillTracer();
			Debug.Log("hit: " + hit.collider.name + " for: " + Damage);
		}
	}

	void Effect () {
		Instantiate (BulletTracerPrefab, firePoint.position, firePoint.rotation);
		Transform clone = Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		clone.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.1f);
	}
}
