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

	public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
	{
		return new Vector3 (lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
	}

	public static float Dot(Vector3 lhs, Vector3 rhs)
	{
		return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
	}
		
}
