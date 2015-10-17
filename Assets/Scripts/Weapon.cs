using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public int damage = 10;
	public LayerMask whatToHit;
	public Transform BulletTracerPrefab;
	public float effectSpawnRate = 10;
	public Transform MuzzleFlashPrefab;
	public Transform HitPrefab;

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

		if (hit.collider != null) {
			// TODO: Kill things if they hit floor

			Enemy enemy = hit.collider.GetComponent<Enemy>();
			if (enemy != null) {
				enemy.DamageEnemy(damage);
				Debug.Log("hit: " + hit.collider.name + " for: " + damage);
			}
		}

		if (Time.time >= timeToSpawnEffect) {
			Vector3 hitPos;
			Vector3 hitNormal;

			if (hit.collider == null) {
				hitPos = (mousePosition - firePointPosition) * 30;
				hitNormal = new Vector3 (9999,9999,9999);
			}
			else {
				hitPos = hit.point;
				hitNormal = hit.normal;
			}

			Effect(hitPos, hitNormal);
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}
	}

	void Effect (Vector3 hitPos, Vector3 hitNormal) {
		Transform trail = Instantiate (BulletTracerPrefab, firePoint.position, firePoint.rotation) as Transform;
		LineRenderer lr = trail.GetComponent<LineRenderer>();

		if (lr != null) {
			lr.SetPosition (0, firePoint.position);
			lr.SetPosition (1, hitPos);
		}

		Destroy (trail.gameObject, 0.1f);

		if (hitNormal != new Vector3 (9999,9999,9999)) {
			Transform hitParticle = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
			Destroy (hitParticle.gameObject, 0.2f);
		}

		Transform clone = Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		clone.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.1f);
	}
}
