using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	private Camera cameraComponent;

	void Start()
	{
		cameraComponent = GetComponent<Camera> ();
	}

	void Update () 
	{
		if (target)
		{
			Vector3 point = cameraComponent.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - cameraComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}