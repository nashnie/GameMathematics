using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour 
{
	public Vector3 start;
	public Vector3 direction;
	public float length;
	public Transform directionTarget;

	private Vector3 end;

	// Use this for initialization
	void Awake () {
		start = transform.position;
		direction = (directionTarget.position - start).normalized;
		end = start + direction.normalized * length;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(start, 1f);
		Debug.DrawLine (start, end, Color.yellow);
	}
}
