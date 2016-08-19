using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UrbanBehaviour : MonoBehaviour {
	public DroneBehaviour player;

	private float diffMultiplier = 0.5f;

	// Use this for initialization
	void Start () {
		diffMultiplier *= PlayerPrefs.GetInt ("playerDiff");

		if (player) {
			player.SetPlayerScore (PlayerPrefs.GetInt ("lastScore"));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (player && player.IsCrashed ()) {
			StartCoroutine (WaitAndStartNextLevel(2.0f));
		}

		if (player && player.GetPlayerScore () >= 90) {
			PlayerPrefs.SetInt ("lastScore", player.GetPlayerScore());
			SceneManager.LoadScene ("BestScore");
		}
	}

	IEnumerator WaitAndStartNextLevel(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		PlayerPrefs.SetInt ("lastScore", player.GetPlayerScore());
		SceneManager.LoadScene ("BestScore");
	}
}
