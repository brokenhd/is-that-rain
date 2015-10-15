using UnityEngine;
using System.Collections;

public class TurnPlayer : MonoBehaviour {
	
	public GameObject visualChild;
	int screenWidth = Screen.width;
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos = Input.mousePosition;
		if(mousePos.x >= screenWidth / 2) {
			visualChild.transform.localScale = Vector3.one;
		} else {
			Vector3 newScale = Vector3.one;
			newScale.x = -1;
			visualChild.transform.localScale = newScale;
		}
	}
}
