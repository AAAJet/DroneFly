using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ForestBehaviour : MonoBehaviour {
	public DroneBehaviour player;

	public GameObject tree1;
	public GameObject tree2;
	public GameObject rock1;
	public GameObject rock2;

	private int rocksMaxCount = 20;
	private int treesMaxCount = 10;

	private float diffMultiplier = 0.5f;

	private float minY;
	private float maxY;

	// Use this for initialization
	void Start () {
		diffMultiplier *= PlayerPrefs.GetInt ("playerDiff");
		treesMaxCount = treesMaxCount + (int)(treesMaxCount * diffMultiplier);

		minY = -8.0f;
		maxY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height, 0)).y;

		GenerateForest ();

		if (player) {
			player.SetPlayerScore (PlayerPrefs.GetInt ("lastScore"));
		}
	}

	private void GenerateForest()
	{
		int rocksCount = Random.Range ((int)(rocksMaxCount * 0.75f), rocksMaxCount + 1);
		int treesCount = Random.Range ((int)(treesMaxCount * 0.75f), treesMaxCount + 1);

		for (int i = 0; i <= rocksCount; i++) {
			if (i % 2 == 0) {
				Instantiate (rock1, GetRandomPos(), Quaternion.identity);
			} else {
				Instantiate (rock2, GetRandomPos(), Quaternion.identity);
			}
		}

		for (int i = 0; i <= treesCount; i++) {
			if (i % 2 == 0) {
				Instantiate (tree1, GetRandomPos (), Quaternion.identity);
			} else {
				Instantiate (tree2, GetRandomPos (), Quaternion.identity);
			}
		}
	}

	private Vector3 GetRandomPos()
	{
		Vector3 randomPos = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, Screen.width), 0, Camera.main.farClipPlane / 2));
		randomPos.y = Random.Range (minY, maxY + 1);
		return randomPos;
	}

	// Update is called once per frame
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
