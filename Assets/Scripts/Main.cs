using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public Point pt;
	public Line line;
    public Line line2;
    public Plane plane;
    public Triangle triangle;

    // Use this for initialization
    void Start () {
		float distance = GameMathematicsStatics.CalculatePointAndLineDistance (pt, line);
		Debug.Log ("CalculatePointAndLineDistance distance " + distance);

        float lineAndLineDistance = GameMathematicsStatics.CalculateLineAndLineDistance(line, line2);
        Debug.Log("CalculateLineAndLineDistance distance " + lineAndLineDistance);

        Vector3 lineAndPlanePoint = GameMathematicsStatics.CalculateLineAndPlaneIntersection(line, plane);
        plane.PlaneAndLinePoint = lineAndPlanePoint;
        Debug.Log("CalculatePointAndLineDistance lineAndPlanePoint " + lineAndPlanePoint.ToString());

        bool isInTriangle = false;
        Vector3 lineAndTrianglePoint = GameMathematicsStatics.CalculateLineAndTriangleIntersection(line, triangle, ref isInTriangle);

        triangle.PlaneAndLinePoint = lineAndTrianglePoint;
        Debug.Log("CalculateLineAndTriangleIntersection lineAndTrianglePoint " + lineAndTrianglePoint.ToString() + " isInTriangle " + isInTriangle);

        int compressedFloat = CompressFloatUtil.CompressFloat(1234.56789f);
        Debug.Log("CompressFloatUtil compressedFloat " + compressedFloat);

        float decompressedFloat = CompressFloatUtil.DecompressFloat(compressedFloat);
        Debug.Log("CompressFloatUtil decompressedFloat " + decompressedFloat);
    }
}
