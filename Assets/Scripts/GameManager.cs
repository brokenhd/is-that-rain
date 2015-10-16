using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager gm;
	public Transform playerPrefab;
	public Transform spawnPoint;
	public int spawnDelay = 2;
	public Transform spawnPrefab;

	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
		}
	}
	
	public IEnumerator RespawnPlayer () {
		yield return new WaitForSeconds (spawnDelay);
		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		GameObject clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
		Destroy (clone, 1f);
	}

	public static void KillPlayer (Player player) {
		Destroy (player.gameObject);


		// Later turn this method into a menu
		gm.StartCoroutine(gm.RespawnPlayer());
	}


}
