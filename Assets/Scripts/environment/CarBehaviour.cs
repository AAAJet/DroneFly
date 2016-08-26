using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {
	public enum TRAVEL_DIR {VERTICAL, HORIZONTAL};
	public TRAVEL_DIR travelDirection;

	public Vector3 startPos;
	public Vector3 endPos;

	private Vector3 randomStartPos;
	private Vector3 randomEndPos;

	public float carSpeed;
	private float diffMultiplier = 0.5f;

	public bool useLocalPosition;

	private Vector3 distVec;

	public void ModifyCarPosRuntime (Vector3 posMidifier)
	{
		transform.position = transform.position - posMidifier;
		randomEndPos.y = randomEndPos.y - posMidifier.y;

		if (randomEndPos.x > 0 && posMidifier.x > 0) {
			randomEndPos.x = randomEndPos.x + posMidifier.x;
		} else if (randomEndPos.x > 0 && posMidifier.x < 0) {
			randomEndPos.x = randomEndPos.x - posMidifier.x;
		} else if (randomEndPos.x < 0 && posMidifier.x > 0) {
			randomEndPos.x = randomEndPos.x - posMidifier.x;
		} else {
			randomEndPos.x = randomEndPos.x + posMidifier.x;
		}
	}

	void OnEnable ()
	{
		if (travelDirection == TRAVEL_DIR.VERTICAL) {
			randomStartPos.x = startPos.x + Random.Range(-1.0f, 1.0f);
			randomEndPos.x = randomStartPos.x;

			randomStartPos.y = startPos.y;
			randomEndPos.y = endPos.y;
		}
		else if (travelDirection == TRAVEL_DIR.HORIZONTAL)
		{
			randomStartPos.x = startPos.x;
			randomEndPos.x = endPos.x;

			randomStartPos.y = startPos.y + Random.Range(-0.25f, 0.25f);
			randomEndPos.y = randomStartPos.y;
		}

		diffMultiplier *= PlayerPrefs.GetInt ("playerDiff");
		transform.localPosition = randomStartPos;
	}
	
	void Update () {
		if (!useLocalPosition) {
			distVec = randomEndPos - transform.position;
		} else {
			distVec = randomEndPos - transform.localPosition;
		}

		if (distVec.sqrMagnitude >= 1.0f) {
			if (!useLocalPosition) {
				transform.position = Vector3.MoveTowards (transform.position, randomEndPos, (carSpeed + carSpeed * diffMultiplier) * Time.deltaTime);
			} else {
				transform.localPosition = Vector3.MoveTowards (transform.localPosition, randomEndPos, (carSpeed + carSpeed * diffMultiplier) * Time.deltaTime);
			}
		} else {
			gameObject.SetActive (false);
		}
	}
}
