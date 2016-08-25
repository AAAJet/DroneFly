using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour {
	public GameObject dronesPanel;
	public GameObject difficultyPanel;
	public GameObject nameEnterPanel;

	public Text nameInput;

	void Start ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	public void OnNameEntered ()
	{
		if (!nameInput.text.Equals ("")) {
			PlayerPrefs.SetString ("playerName", nameInput.text);

			nameEnterPanel.SetActive (false);
			dronesPanel.SetActive (true);
		}
	}

	public void OnDroneSelected (int droneId)
	{
		PlayerPrefs.SetInt ("playerDrone", droneId);

		dronesPanel.SetActive (false);
		difficultyPanel.SetActive (true);
	}

	public void OnDifficultySelected (int diffId)
	{
		PlayerPrefs.SetInt ("playerDiff", diffId);

		SceneManager.LoadScene ("Level1");
	}
}
