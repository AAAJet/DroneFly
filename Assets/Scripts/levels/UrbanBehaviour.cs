using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UrbanBehaviour : BaseLevelBehaviour {
	public GameObject house1;
	public GameObject house2;
	public GameObject house3;

	private float diffMultiplier = 1.0f;

	private List<BoxCollider2D> leaveAreas = new List<BoxCollider2D> ();

	private GameObject carsContainer;

	private BoxCollider2D leaveArea1;
	private BoxCollider2D leaveArea2;
	private BoxCollider2D leaveArea3;

	public List<GameObject> cars = new List<GameObject>();
	private int prevRandCarIndex = -1;
	private float carWaitTime = 1.5f;
	public static bool isCarsStarted;

	void OnEnable () {
		carsContainer = GameObject.Find ("CarsContainer");
		diffMultiplier *= PlayerPrefs.GetInt ("playerDiff");
		carWaitTime = carWaitTime - diffMultiplier;

		leaveArea1 = transform.Find ("LeaveArea1").GetComponent<BoxCollider2D> ();
		leaveArea2 = transform.Find ("LeaveArea2").GetComponent<BoxCollider2D> ();
		leaveArea3 = transform.Find ("LeaveArea3").GetComponent<BoxCollider2D> ();
		leaveAreas.Add (leaveArea1);
		leaveAreas.Add (leaveArea2);
		leaveAreas.Add (leaveArea3);

		GenerateObstacles ();

		if (player) {
			player.SetPlayerScore (PlayerPrefs.GetInt ("lastScore"));
		}

		if (!isCarsStarted) {
			StartCoroutine (StartRandomCar ());
			isCarsStarted = true;
		}
	}

	#region Urban Cars Obstacles
	private GameObject GetRandomCar()
	{
		GameObject res = null;

		int rand = Random.Range (0, cars.Count);

		while (prevRandCarIndex == rand) {
			rand = Random.Range (0, cars.Count);
		}

		if (!cars [rand].activeInHierarchy) {
			res = cars [rand];
		}

		prevRandCarIndex = rand;

		return res;
	}

	IEnumerator StartRandomCar()
	{
		while (!player.IsCrashed ()) {
			yield return new WaitForSeconds (carWaitTime);

			if (!player.IsCrashed ()) {
				GameObject newCar = GetRandomCar ();

				while (newCar == null) {
					yield return new WaitForSeconds (0.1f);
					newCar = GetRandomCar ();
				}

				newCar.SetActive (true);
			}
		}
	}
	#endregion

	#region House Obstacles
	protected override void GenerateObstacles()
	{
		int leaveAreaRandIndex;

		if (currPos == BACKGROUND_POS.CENTER) {
			leaveAreas.RemoveAt (0);
		}
		else if (diffMultiplier == 0) {
			leaveAreaRandIndex = Random.Range (0, leaveAreas.Count);
			leaveAreas.RemoveAt (leaveAreaRandIndex);
		}

		while (leaveAreas.Count > 0) {
			leaveAreaRandIndex = Random.Range (0, leaveAreas.Count);
			int houseRandType = Random.Range (1, 3 + 1);

			Vector3 randomHousePos = GetRandomObstaclePos (leaveAreas [leaveAreaRandIndex]);

			if (houseRandType == 1) {
				backgroundObjects.Add (Instantiate (house1, randomHousePos, Quaternion.identity, transform) as GameObject);
			} else if (houseRandType == 2) {
				backgroundObjects.Add (Instantiate (house2, randomHousePos, Quaternion.identity, transform) as GameObject);
			} else if (houseRandType == 3) {
				backgroundObjects.Add (Instantiate (house3, randomHousePos, Quaternion.identity, transform) as GameObject);
			}

			leaveAreas.RemoveAt (leaveAreaRandIndex);
		}
	}

	protected override Vector3 GetRandomObstaclePos(BoxCollider2D curLeaveArea)
	{
		Vector3	randomPos = new Vector3 (Random.Range (curLeaveArea.bounds.min.x, curLeaveArea.bounds.max.x)
			, Random.Range (curLeaveArea.bounds.min.y, curLeaveArea.bounds.max.y)
			, 0);

		return randomPos;
	}

	protected override void MutateObstacles()
	{
		leaveAreas.Add (leaveArea1);
		leaveAreas.Add (leaveArea2);
		leaveAreas.Add (leaveArea3);

		int leaveAreaRandIndex;
		if (diffMultiplier == 0) {
			leaveAreaRandIndex = Random.Range (0, leaveAreas.Count);
			leaveAreas.RemoveAt (leaveAreaRandIndex);
		}

		foreach (GameObject go in backgroundObjects)
		{
			if (leaveAreas.Count > 0) {
				leaveAreaRandIndex = Random.Range (0, leaveAreas.Count);
				int houseRandType = Random.Range (1, 3 + 1);

				Vector3 randomHousePos = GetRandomObstaclePos (leaveAreas [leaveAreaRandIndex]);
				go.transform.position = randomHousePos;

				leaveAreas.RemoveAt (leaveAreaRandIndex);
			}
		}
	}

	public override void ChangePosition(Vector3 centerPos, Vector3 centerSize, BACKGROUND_POS newPosition, bool mutateObstacles)
	{
		base.ChangePosition (centerPos, centerSize, newPosition, mutateObstacles);

		if (currPos == BaseLevelBehaviour.BACKGROUND_POS.CENTER) {
			Vector3 containerNewPosDelta = transform.position - carsContainer.transform.position;
			carsContainer.transform.position = transform.position;

			foreach (GameObject car in cars) {
				if (car.activeInHierarchy) {
					car.GetComponent<CarBehaviour> ().ModifyCarPosRuntime (containerNewPosDelta);
				}
			}
		}
	}
	#endregion
}
