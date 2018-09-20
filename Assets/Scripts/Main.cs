using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public Point pt;
	public Line line;
    public Plane plane;

	// Use this for initialization
	void Start () {
		float distance = GameMathematicsStatics.CalculatePointAndLineDistance (pt, line);
		Debug.Log ("CalculatePointAndLineDistance distance " + distance);

        Vector3 lineAndPlanePoint = GameMathematicsStatics.CalculateLineAndPlanePoint(line, plane);
        plane.PlaneAndLinePoint = lineAndPlanePoint;
        Debug.Log("CalculatePointAndLineDistance lineAndPlanePoint " + lineAndPlanePoint.ToString());
    }
}
