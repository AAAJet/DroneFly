using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UrbanBehaviour : BaseLevelBehaviour {
	/*public GameObject tree1;
	public GameObject tree2;
	public GameObject rock1;
	public GameObject rock2;

	private int rocksMaxCount = 30;
	private int treesMaxCount = 12;

	private float diffMultiplier = 1.0f;*/

	// Use this for initialization
	void OnEnable () {
		/*diffMultiplier *= PlayerPrefs.GetInt ("playerDiff");
		treesMaxCount = treesMaxCount + (int)(treesMaxCount * diffMultiplier);*/

		GenerateObstacles ();

		if (player) {
			player.SetPlayerScore (PlayerPrefs.GetInt ("lastScore"));
		}
	}

	protected override void GenerateObstacles()
	{
		/*int rocksCount = Random.Range ((int)(rocksMaxCount * 0.75f), rocksMaxCount + 1);
		int treesCount = Random.Range ((int)(treesMaxCount * 0.75f), treesMaxCount + 1);

		for (int i = 0; i <= rocksCount; i++) {
			if (i % 2 == 0) {
				backgroundObjects.Add(Instantiate (rock1, GetRandomObstaclePos(), Quaternion.identity, transform) as GameObject);
			} else {
				backgroundObjects.Add(Instantiate (rock2, GetRandomObstaclePos(), Quaternion.identity, transform) as GameObject);
			}
		}

		for (int i = 0; i <= treesCount; i++) {
			if (i % 2 == 0) {
				backgroundObjects.Add(Instantiate (tree1, GetRandomObstaclePos (), Quaternion.identity, transform) as GameObject);
			} else {
				backgroundObjects.Add(Instantiate (tree2, GetRandomObstaclePos (), Quaternion.identity, transform) as GameObject);
			}
		}*/
	}

	protected override void MutateObstacles()
	{
		/*foreach (GameObject go in backgroundObjects)
		{
			go.transform.position = GetRandomObstaclePos ();
		}*/
	}

	protected override Vector3 GetRandomObstaclePos()
	{
		/*Vector3 randomPos = startingBoundsCollider.bounds.center;

		while (startingBoundsCollider.bounds.Contains(randomPos))
		{
			randomPos = new Vector3 (Random.Range (levelBoundsCollider.bounds.min.x, levelBoundsCollider.bounds.max.x)
				, Random.Range (levelBoundsCollider.bounds.min.y, levelBoundsCollider.bounds.max.y)
				, 0);
		}

		return randomPos;*/
		return new Vector3 ();
	}
}
