using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseLevelBehaviour : MonoBehaviour {
	public enum BACKGROUND_POS {LOWER_LEFT, LOWER_CENTER, LOWER_RIGHT, LEFT, CENTER, RIGHT, UPPER_LEFT, UPPER_CENTER, UPPER_RIGHT};
	public BACKGROUND_POS currPos;

	public DroneBehaviour player;

	protected bool isLocked;

	protected List <GameObject> backgroundObjects = new List<GameObject> ();

	protected Collider2D levelBoundsCollider;
	protected Collider2D startingBoundsCollider;
	protected ProgressManager progressManager;

	void Awake ()
	{
		levelBoundsCollider = GetComponent<Collider2D> ();
		startingBoundsCollider = player.transform.Find ("startingBounds").GetComponent<Collider2D>();
		progressManager = GameObject.FindGameObjectWithTag ("ProgressManager").GetComponent("ProgressManager") as ProgressManager;
	}

	public void LockElement (bool isLocked)
	{
		this.isLocked = isLocked;
	}

	public bool IsLocked()
	{
		return this.isLocked;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (!currPos.Equals (BACKGROUND_POS.CENTER)) {
			progressManager.OnNewBackgroundEntered (this);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (currPos == BACKGROUND_POS.CENTER)
			progressManager.EnableBackgroundsColliders ();
	}

	public virtual void ChangePosition(Vector3 centerPos, Vector3 centerSize, BACKGROUND_POS newPosition, bool mutateObstacles)
	{
		currPos = newPosition;

		if (mutateObstacles)
		{
			switch (currPos)
			{
			case BACKGROUND_POS.RIGHT:
				transform.position = new Vector3(centerPos.x + centerSize.x, centerPos.y, centerPos.z);
				break;
			case BACKGROUND_POS.LEFT:
				transform.position = new Vector3(centerPos.x - centerSize.x, centerPos.y, centerPos.z);
				break;
			case BACKGROUND_POS.UPPER_RIGHT:
				transform.position = new Vector3(centerPos.x + centerSize.x, centerPos.y + centerSize.y, centerPos.z);
				break;
			case BACKGROUND_POS.UPPER_CENTER:
				transform.position = new Vector3(centerPos.x, centerPos.y + centerSize.y, centerPos.z);
				break;
			case BACKGROUND_POS.UPPER_LEFT:
				transform.position = new Vector3(centerPos.x - centerSize.x, centerPos.y + centerSize.y, centerPos.z);
				break;
			case BACKGROUND_POS.LOWER_RIGHT:
				transform.position = new Vector3 (centerPos.x + centerSize.x, centerPos.y - centerSize.y, centerPos.z);
				break;
			case BACKGROUND_POS.LOWER_CENTER:
				transform.position = new Vector3(centerPos.x, centerPos.y - centerSize.y, centerPos.z);
				break;
			case BACKGROUND_POS.LOWER_LEFT:
				transform.position = new Vector3(centerPos.x - centerSize.x, centerPos.y - centerSize.y, centerPos.z);
				break;
			}

			MutateObstacles ();
		}
	}

	protected virtual void GenerateObstacles()
	{
	}

	protected virtual void MutateObstacles()
	{
	}

	protected virtual Vector3 GetRandomObstaclePos()
	{
		return new Vector3();
	}

	protected virtual Vector3 GetRandomObstaclePos(BoxCollider2D spawnArea)
	{
		return new Vector3();
	}
}
