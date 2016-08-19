using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class BestScoresBehaviour : MonoBehaviour {
	public Text[] scoreLabels;

	private struct Score {
		public string playerName;
		public int playerScore;

		public Score (string name, int score)
		{
			playerName = name;
			playerScore = score;
		}
	}

	private List<Score> scoreValues = new List<Score>();

	void Start ()
	{
		string playerName = PlayerPrefs.GetString ("playerName");
		int lastScore = PlayerPrefs.GetInt ("lastScore");

		for (int i = 0; i <= 4; i++) {
			int savedScore = PlayerPrefs.GetInt ("scorePlayer" + i);
			string savedPlayer = PlayerPrefs.GetString ("namePlayer" + i);

			if (savedPlayer.Equals ("")) {
				savedPlayer = "Player " + (i + 1);
			}

			if (savedScore > lastScore) {
				scoreValues.Add (new Score(savedPlayer, savedScore));
			} else {
				scoreValues.Add (new Score(playerName, lastScore));
				scoreValues.Add (new Score(savedPlayer, savedScore));
				lastScore = -1;
			}
		}

		if (scoreValues.Count > 5) {
			scoreValues.RemoveAt (scoreValues.Count - 1);
		}

		int labelNum = 0;
		foreach (Score entry in scoreValues)
		{
			scoreLabels [labelNum].text = entry.playerName + " - " + entry.playerScore;
			PlayerPrefs.SetString ("namePlayer" + labelNum, entry.playerName);
			PlayerPrefs.SetInt ("scorePlayer" + labelNum, entry.playerScore);
			labelNum++;
		}

		PlayerPrefs.Save ();
	}

	public void OnRestartPressed()
	{
		SceneManager.LoadScene ("MainMenu");
	}
}
