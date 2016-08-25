using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {

	private Vector3 screenUp;
	private Vector3 screenDown;
	private Vector3 startPos;
	private Vector3 endPos;
	float carHeight;

	private float carSpeed = 10.0f;
	private float diffMultiplier = 0.5f;

	void OnEnable ()
	{
		diffMultiplier *= PlayerPrefs.GetInt ("playerDiff");

		carHeight = GetComponent<SpriteRenderer> ().sprite.bounds.size.y;

		screenDown = Camera.main.ScreenToWorldPoint (new Vector3(0, 0, 0));
		screenUp = Camera.main.ScreenToWorldPoint (new Vector3(0, Screen.height, 0));

		startPos = new Vector3 (transform.position.x, screenDown.y - carHeight, 0);
		endPos = new Vector3 (transform.position.x, screenUp.y + carHeight, 0);

		transform.position = startPos;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= screenUp.y + carHeight / 2) {
			transform.position = Vector3.MoveTowards (transform.position, endPos, (carSpeed + carSpeed * diffMultiplier) * Time.deltaTime);
		} else {
			gameObject.SetActive (false);
		}
	}
}
