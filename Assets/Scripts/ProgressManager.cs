using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ProgressManager : MonoBehaviour {
	public DroneBehaviour player;

	private GameObject[] backgrounds = new GameObject[9];
	private GameObject prevCenterObj;

	public int maxLevelPoints;
	public string nextLevelName;

	void Start ()
	{
		for (int i = 0; i <= 8; i++) {
			backgrounds [i] = GameObject.Find ("levelBackground" + (i+1));
		}
	}

	public void OnNewBackgroundEntered (BaseLevelBehaviour newBackgroundObj)
	{
		prevCenterObj = GetBackByPos (BaseLevelBehaviour.BACKGROUND_POS.CENTER, false).gameObject;

		Vector3 centerPos = newBackgroundObj.gameObject.GetComponent<BoxCollider2D>().bounds.center;
		Vector3 centerSize = newBackgroundObj.gameObject.GetComponent<BoxCollider2D>().bounds.size;
		newBackgroundObj.LockElement (true);

		switch (newBackgroundObj.currPos) {
		case BaseLevelBehaviour.BACKGROUND_POS.RIGHT:
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.RIGHT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LEFT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER, false);
            break;
		case BaseLevelBehaviour.BACKGROUND_POS.LEFT:
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.RIGHT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT, true);
            break;
		case BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER:
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LEFT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.RIGHT, false);
            break;
		case BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER:
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LEFT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.RIGHT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT, true);
            break;
		case BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT:
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.RIGHT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LEFT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT, false);
            break;
		case BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT:
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.RIGHT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT, true);
            break;
		case BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT:
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT).ChangePosition(centerPos, centerSize, ForestBehaviour.BACKGROUND_POS.LOWER_LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER).ChangePosition(centerPos, centerSize, ForestBehaviour.BACKGROUND_POS.LEFT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.CENTER).ChangePosition(centerPos, centerSize, ForestBehaviour.BACKGROUND_POS.UPPER_LEFT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LEFT).ChangePosition(centerPos, centerSize, ForestBehaviour.BACKGROUND_POS.LOWER_CENTER, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.RIGHT).ChangePosition(centerPos, centerSize, ForestBehaviour.BACKGROUND_POS.UPPER_CENTER, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT).ChangePosition(centerPos, centerSize, ForestBehaviour.BACKGROUND_POS.LOWER_RIGHT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER).ChangePosition(centerPos, centerSize, ForestBehaviour.BACKGROUND_POS.RIGHT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT).ChangePosition(centerPos, centerSize, ForestBehaviour.BACKGROUND_POS.UPPER_RIGHT, true);
            break;
		case BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT:
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.RIGHT, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_RIGHT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER, false);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_CENTER, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.UPPER_LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_CENTER).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LEFT, true);
			GetBackByPos(BaseLevelBehaviour.BACKGROUND_POS.UPPER_RIGHT).ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.LOWER_LEFT, true);
            break;
		}

		newBackgroundObj.ChangePosition(centerPos, centerSize, BaseLevelBehaviour.BACKGROUND_POS.CENTER, false);

		//UnlockElements
		for (int i = 0; i <= 8; i++) {
			backgrounds [i].GetComponent<BaseLevelBehaviour> ().LockElement (false);
		}

		prevCenterObj.GetComponent<BoxCollider2D> ().enabled = false;
    }

	public void EnableBackgroundsColliders()
	{
		for (int i = 0; i <= 8; i++) {
			backgrounds [i].GetComponent<BoxCollider2D> ().enabled = true;
		}
	}

	private BaseLevelBehaviour GetBackByPos (BaseLevelBehaviour.BACKGROUND_POS position, bool useLock)
	{
		BaseLevelBehaviour res = null;

		for (int i = 0; i <= 8; i++)
		{
			BaseLevelBehaviour currScript = backgrounds[i].GetComponent<BaseLevelBehaviour>() as BaseLevelBehaviour;
			if (!currScript.IsLocked() && currScript.currPos == position)
			{
				res = currScript;

				if (useLock)
					res.LockElement (true);
				break;
			}
		}

		return res;
	}

	private BaseLevelBehaviour GetBackByPos (BaseLevelBehaviour.BACKGROUND_POS position)
    {
		return GetBackByPos (position, true);
    }

	void Update () {
		if (player && player.IsCrashed ()) {
			StartCoroutine (WaitAndOpenBestScores(2.0f));
		}

		if (player && player.GetPlayerScore () >= maxLevelPoints) {
			PlayerPrefs.SetInt ("lastScore", player.GetPlayerScore());
			SceneManager.LoadScene (nextLevelName);
		}
	}

	IEnumerator WaitAndOpenBestScores(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		PlayerPrefs.SetInt ("lastScore", player.GetPlayerScore());
		SceneManager.LoadScene ("BestScore");
	}
}
