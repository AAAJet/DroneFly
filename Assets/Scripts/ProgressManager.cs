using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ProgressManager : MonoBehaviour {
	public DroneBehaviour player;

	private GameObject[] forestBackgrounds = new GameObject[9];
	private ForestBehaviour centerBackgroundScript;

	void Start ()
	{
		for (int i = 0; i <= 8; i++) {
			forestBackgrounds [i] = GameObject.Find ("forestBackground" + (i+1));

			ForestBehaviour script = forestBackgrounds [i].GetComponent<ForestBehaviour> ();
			if (script.currPos == ForestBehaviour.BACKGROUND_POS.CENTER) {
				centerBackgroundScript = script;
			}
		}
	}

	public void OnNewBackgroundEntered (ForestBehaviour newForest)
	{
		Vector3 center = newForest.gameObject.GetComponent<BoxCollider2D> ().bounds.center;
		Vector3 size = newForest.gameObject.GetComponent<BoxCollider2D> ().bounds.size;

		switch (newForest.currPos) {
		case ForestBehaviour.BACKGROUND_POS.RIGHT:
				break;
		case ForestBehaviour.BACKGROUND_POS.LEFT:
				break;
		case ForestBehaviour.BACKGROUND_POS.UPPER_CENTER:
				break;
		case ForestBehaviour.BACKGROUND_POS.LOWER_CENTER:
				break;
		case ForestBehaviour.BACKGROUND_POS.UPPER_RIGHT:
				break;
		case ForestBehaviour.BACKGROUND_POS.UPPER_LEFT:
				break;
		case ForestBehaviour.BACKGROUND_POS.LOWER_RIGHT:
				break;
		case ForestBehaviour.BACKGROUND_POS.LOWER_LEFT:
				break;
		}

		centerBackgroundScript = newForest;
	}

	void Update () {
		if (player && player.IsCrashed ()) {
			StartCoroutine (WaitAndStartNextLevel(2.0f));
		}

		if (player && player.GetPlayerScore () >= 60) {
			PlayerPrefs.SetInt ("lastScore", player.GetPlayerScore());
			SceneManager.LoadScene ("Level3");
		}
	}

	IEnumerator WaitAndStartNextLevel(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		PlayerPrefs.SetInt ("lastScore", player.GetPlayerScore());
		SceneManager.LoadScene ("BestScore");
	}
}
