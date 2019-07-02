using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public Point pt;
	public Line line;
    public Line line2;
    public Line sphereCheckLine;

    public Plane plane;

    public Triangle triangle;
    public Sphere sphere;

    public Plane slicePlane;
    public GameObject neededSliceMesh;

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

        bool isInSphere = false;
        Vector3 lineAndSphereIntersection = GameMathematicsStatics.CalculateLineAndSphereIntersection(sphereCheckLine, sphere, ref isInSphere);
        sphere.LineAndSphereIntersection = lineAndSphereIntersection;
        Debug.Log("CalculateLineAndSphereIntersection lineAndSphereIntersection " + lineAndSphereIntersection.ToString() + " isInSphere " + isInSphere);

        Debug.Log("Newton's Method Sqrt " + GameMathematicsStatics.Sqrt(1.23456) + " Normal Sqrt " + System.Math.Sqrt(1.23456));

        GameMathematicsStatics.SliceMesh(slicePlane, neededSliceMesh.transform);
    }
}
