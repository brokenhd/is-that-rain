using UnityEngine;
using System.Collections;

public class MoveTracer : MonoBehaviour {

	public int tracerSpeed = 230;
	public string sortingLayer;

	private Renderer getMeshRenderer() {
		return gameObject.GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {

		if(getMeshRenderer().sortingLayerName != sortingLayer && sortingLayer != "") {
			getMeshRenderer().sortingLayerName = sortingLayer;
		}

		transform.Translate(Vector3.right * Time.deltaTime * tracerSpeed);
		KillTracer();
	}

	public void KillTracer () {
		Destroy(gameObject, 1);
	}
}
