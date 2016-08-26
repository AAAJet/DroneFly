using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class RoadBehaviour : MonoBehaviour {
	public DroneBehaviour player;

	public List<GameObject> cars = new List<GameObject> ();

	private float diffMultiplier = 0.5f;
	private float carWaitTime = 2.0f;

	private int prevRandCarIndex = -1;

	// Use this for initialization
	void Start () {
		diffMultiplier *= PlayerPrefs.GetInt ("playerDiff");
		carWaitTime = carWaitTime - diffMultiplier;

		StartCoroutine (StartRandomCar());

		//player.SetPlayerScore (30);
	}
	
	// Update is called once per frame
	void Update () {
		if (player && player.IsCrashed ()) {
			StartCoroutine (WaitAndStartNextLevel(2.0f));
		}

		if (player && player.GetPlayerScore () >= 30) {
			PlayerPrefs.SetInt ("lastScore", player.GetPlayerScore());
			SceneManager.LoadScene ("Level2");
		}
	}

	IEnumerator WaitAndStartNextLevel(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		PlayerPrefs.SetInt ("lastScore", player.GetPlayerScore());
		SceneManager.LoadScene ("BestScore");
	}

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
}
