using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public Point pt;
	public Line line;
    public Line line2;
    public Plane plane;

	// Use this for initialization
	void Start () {
		float distance = GameMathematicsStatics.CalculatePointAndLineDistance (pt, line);
		Debug.Log ("CalculatePointAndLineDistance distance " + distance);

        float lineAndLineDistance = GameMathematicsStatics.CalculateLineAndLineDistance(line, line2);
        Debug.Log("CalculateLineAndLineDistance distance " + lineAndLineDistance);

        Vector3 lineAndPlanePoint = GameMathematicsStatics.CalculateLineAndPlanePoint(line, plane);
        plane.PlaneAndLinePoint = lineAndPlanePoint;
        Debug.Log("CalculatePointAndLineDistance lineAndPlanePoint " + lineAndPlanePoint.ToString());

        //1233.333
        int compressedFloat = CompressFloatUtil.CompressFloat(1234.56789f);
        Debug.Log("CompressFloatUtil compressedFloat " + compressedFloat);

        float decompressedFloat = CompressFloatUtil.DecompressFloat(compressedFloat);
        Debug.Log("CompressFloatUtil decompressedFloat " + decompressedFloat);
    }
}
