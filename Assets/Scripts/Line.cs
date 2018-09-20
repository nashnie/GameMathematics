using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour 
{
	public Vector3 start;
	public Vector3 direction;
	public float length;
	public Transform directionTarget;

    public Vector3 end;

    public Color DrawGizmosColor = Color.yellow;

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
		Debug.DrawLine (start, end, DrawGizmosColor);
	}
}
