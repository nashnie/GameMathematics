using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

    public float x;
    public float y;
    public float z;

    public Vector3 location = Vector3.zero;

    // Use this for initialization
	void Awake () {
		location = transform.position;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(location, 1.5f);
	}
}
