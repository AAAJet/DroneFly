using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DroneBehaviour : MonoBehaviour 
{
	Rigidbody2D rigidBody;
	private bool isCrashed = false;
	private float collectedPoints;
	private Text scoreTxt;

	private float diffMultiplier = 0.5f;

	public Sprite[] allSprites;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sprite = allSprites[PlayerPrefs.GetInt ("playerDrone")];
		rigidBody = GetComponent<Rigidbody2D> ();
		scoreTxt = GameObject.FindGameObjectWithTag ("Score").GetComponent<Text>();
		diffMultiplier *= PlayerPrefs.GetInt ("playerDiff");

		if (Application.isEditor) {
			GetComponent<BoxCollider2D> ().isTrigger = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsCrashed()
	{
		return isCrashed;
	}

	public void SetPlayerScore (int score)
	{
		collectedPoints = score;
	}

	public int GetPlayerScore ()
	{
		return (int)collectedPoints;
	}

	void FixedUpdate(){
		if (Application.isEditor) {
			Vector2 force = new Vector2 (0.5f, 0);
			rigidBody.AddForce (force * 10);
			return;
		}

		if (!isCrashed) {
			Vector2 force = new Vector2 (Input.acceleration.x, Input.acceleration.y);
			rigidBody.AddForce (force * 10);

			float pointsInc = force.SqrMagnitude () / 10;
			collectedPoints = collectedPoints + pointsInc + pointsInc * diffMultiplier;
			scoreTxt.text = "Points : " + (int)collectedPoints;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (Application.isEditor) {
			return;
		}

		if (!isCrashed && coll.gameObject.tag.Equals("Obstacle")) {
			GetComponent<ParticleSystem> ().Play ();
			isCrashed = true;
		}
	}
}
