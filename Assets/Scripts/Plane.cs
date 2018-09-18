using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {

	public Vector3 planeNormal;
	public float distanceFromOrigin;
	public Vector3 planeCenter;

	public Transform directionTarget;

	public GameObject plane;

	public GameObject plane2;

	void Awake () {
		planeCenter = transform.position;
		planeNormal = (directionTarget.position - planeCenter).normalized;
		plane.transform.position = planeCenter;
		Vector3 planeRot = Quaternion.identity * (planeNormal);
		Vector3 planeRot2 = Quaternion.identity * (-planeNormal);
		Debug.Log ("planeRot" + planeRot.ToString());
		Debug.Log ("planeRot2" + planeRot2.ToString());

		plane.transform.localRotation = Quaternion.LookRotation(planeRot);
		plane2.transform.localRotation =  Quaternion.LookRotation(planeRot2);
	}
}
