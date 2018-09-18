using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Nash
/// Game mathematics statics.
/// </summary>
public class GameMathematicsStatics
{
	public static float CalculatePointAndLineDistance(Point pt, Line line)
	{
		Vector3 qs = pt.location - line.start;
		float qssqrMagnitudeLength = qs.sqrMagnitude;
		float vsLength = Dot(qs, line.direction);
		float sqrMagnitudeDLength = qssqrMagnitudeLength - vsLength *  vsLength;
		float length = Mathf.Sqrt (sqrMagnitudeDLength);

		return length;
	}

	//https://www.geometrictools.com/Documentation/DistanceLine3Line3.pdf
	public static float CalculateLineAndLineDistance()
	{
		return -1.0f;
	}

	public static Vector3 CalculateLineAndPlanePoint(Line line, Plane plane)
	{
		float d = -Dot(plane.planeNormal, plane.planeCenter);//-n*p
		float ns = Dot(plane.planeNormal, line.start);//n*s
		float nv = Dot(plane.planeNormal, line.direction);
		if (nv == 0)
		{
			if (ns + d == 0) {
				//baohan
				return Vector3.zero;
			} else {
				//pingxing
				return Vector3.zero;
			}
		}

		float t = -(ns + d) / nv;
		Vector3 point = line.start + line.direction * t;
		return point;
	}
		

	public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
	{
		return new Vector3 (lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
	}

	public static float Dot(Vector3 lhs, Vector3 rhs)
	{
		return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
	}
		
}
