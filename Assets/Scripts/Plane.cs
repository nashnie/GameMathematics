using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {

	public Vector3 planeNormal;
    public Vector3 planeTangent;

    public float distanceFromOrigin;
	public Vector3 planeCenter;

	public Transform directionTarget;

	public GameObject plane;

    protected Vector3 planeAndLinePoint;

    public Vector3 PlaneAndLinePoint
    {
        set
        {
            planeAndLinePoint = value;
            Vector3 direction = (planeAndLinePoint - planeCenter).normalized;
            plane.transform.position = planeCenter;
            Vector3 planeRot = Quaternion.identity * direction;
            Debug.Log("planeRot" + planeRot.ToString());

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = planeAndLinePoint;
            cube.transform.localScale = Vector3.one * 0.1f + Vector3.up;

            plane.transform.localRotation = Quaternion.LookRotation(planeRot);
        }
    }

    protected virtual void Awake ()
    {
		planeCenter = transform.position;
		planeNormal = (directionTarget.position - planeCenter).normalized;
    }
}
