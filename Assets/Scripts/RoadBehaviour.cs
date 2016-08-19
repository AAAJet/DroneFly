using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoadBehaviour : MonoBehaviour {
	public DroneBehaviour player;

	public GameObject car1;
	public GameObject car2;
	public GameObject car3;

	private float diffMultiplier = 0.5f;
	private float carWaitTime = 2.0f;

	// Use this for initialization
	void Start () {
		diffMultiplier *= PlayerPrefs.GetInt ("playerDiff");
		carWaitTime = carWaitTime - diffMultiplier;

		StartCoroutine (StartRandomCar());
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

	IEnumerator StartRandomCar()
	{
		while (!player.IsCrashed ()) {
			yield return new WaitForSeconds (carWaitTime);

			if (!player.IsCrashed ()) {
				int i = Random.Range (1, 3+1);

				if (i == 1 && !car1.activeInHierarchy) {
					car1.SetActive (true);
				} else {
					i = 2;
				}

				if (i == 2 && !car2.activeInHierarchy) {
					car2.SetActive (true);
				} else {
					i = 3;
				}

				if (i == 3 && !car3.activeInHierarchy) {
					car3.SetActive (true);
				}
			}
		}
	}
}
