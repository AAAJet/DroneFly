using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForestBehaviour : MonoBehaviour {
	public enum BACKGROUND_POS {LOWER_LEFT, LOWER_CENTER, LOWER_RIGHT, LEFT, CENTER, RIGHT, UPPER_LEFT, UPPER_CENTER, UPPER_RIGHT};
	public BACKGROUND_POS currPos;

	public DroneBehaviour player;

	public GameObject tree1;
	public GameObject tree2;
	public GameObject rock1;
	public GameObject rock2;

	private List <GameObject> backgroundObjects = new List<GameObject> ();

	private int rocksMaxCount = 30;
	private int treesMaxCount = 15;

	private float diffMultiplier = 0.5f;

	private Collider2D levelBoundsCollider;
	private Collider2D startingBoundsCollider;

	private ProgressManager progressManager;

	void Awake ()
	{
		levelBoundsCollider = GetComponent<Collider2D> ();
		startingBoundsCollider = player.transform.Find ("startingBounds").GetComponent<Collider2D>();
		progressManager = GameObject.FindGameObjectWithTag ("ProgressManager").GetComponent("ProgressManager") as ProgressManager;
	}

	// Use this for initialization
	void OnEnable () {
		diffMultiplier *= PlayerPrefs.GetInt ("playerDiff");
		treesMaxCount = treesMaxCount + (int)(treesMaxCount * diffMultiplier);

		if (backgroundObjects.Count > 0) {
			ChangeForest ();
		} else {
			GenerateForest ();
		}

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
				backgroundObjects.Add(Instantiate (rock1, GetRandomPos(), Quaternion.identity, transform) as GameObject);
			} else {
				backgroundObjects.Add(Instantiate (rock2, GetRandomPos(), Quaternion.identity, transform) as GameObject);
			}
		}

		for (int i = 0; i <= treesCount; i++) {
			if (i % 2 == 0) {
				backgroundObjects.Add(Instantiate (tree1, GetRandomPos (), Quaternion.identity, transform) as GameObject);
			} else {
				backgroundObjects.Add(Instantiate (tree2, GetRandomPos (), Quaternion.identity, transform) as GameObject);
			}
		}
	}

	private void ChangeForest()
	{
		foreach (GameObject go in backgroundObjects)
		{
			go.transform.position = GetRandomPos ();
		}
	}

	private Vector3 GetRandomPos()
	{
		Vector3 randomPos = startingBoundsCollider.bounds.center;

		while (startingBoundsCollider.bounds.Contains(randomPos))
		{
			randomPos = new Vector3 (Random.Range (levelBoundsCollider.bounds.min.x, levelBoundsCollider.bounds.max.x)
				, Random.Range (levelBoundsCollider.bounds.min.y, levelBoundsCollider.bounds.max.y)
				, 0);
		}

		return randomPos;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (!currPos.Equals (BACKGROUND_POS.CENTER)) {
			progressManager.OnNewBackgroundEntered (this);
		}
	}
}
