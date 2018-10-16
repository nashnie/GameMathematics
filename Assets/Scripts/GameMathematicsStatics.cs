using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Nash
/// Game mathematics statics.
/// </summary>
public class GameMathematicsStatics
{
    private static float SMALL_NUM = 0.00000001f;

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
    //http://geomalgorithms.com/a07-_distance.html
    public static float CalculateLineAndLineDistance(Line line1, Line line2)
	{
        return dist3D_Line_to_Line(line1, line2);
	}

	public static Vector3 CalculateLineAndPlaneIntersection(Line line, Plane plane)
	{
		float d = -Dot(plane.planeNormal, plane.planeCenter);//-n*p
		float ns = Dot(plane.planeNormal, line.start);//n*s
		float nv = Dot(plane.planeNormal, line.direction);
		if (nv == 0)
		{
			if (ns + d == 0) {
				//直线被平面包含
				return Vector3.zero;
			} else {
                //直线和平面包含
                return Vector3.zero;
			}
		}

		float t = -(ns + d) / nv;
		Vector3 point = line.start + line.direction * t;
		return point;
	}

    public static Vector3 CalculateLineAndTriangleIntersection(Line line, Triangle plane, ref bool isInTriangle)
    {
        isInTriangle = false;

        float d = -Dot(plane.planeNormal, plane.planeCenter);//-n*p
        float ns = Dot(plane.planeNormal, line.start);//n*s
        float nv = Dot(plane.planeNormal, line.direction);
        if (nv == 0)
        {
            if (ns + d == 0)
            {
                //直线被平面包含
                return Vector3.zero;
            }
            else
            {
                //直线和平面包含
                return Vector3.zero;
            }
        }

        float t = -(ns + d) / nv;
        Vector3 point = line.start + line.direction * t;

        Vector3 r = point - plane.p0.location;
        Vector3 q1 = plane.p1.location - plane.p0.location;
        Vector3 q2 = plane.p2.location - plane.p0.location;
        float q1Square = Dot(q1, q1);
        float q2Square = Dot(q2, q2);
        float q1q2 = Dot(q1, q2);
        float rq1 = Dot(r, q1);
        float rq2 = Dot(r, q2);

        float k = 1 / (q1Square * q2Square - q1q2 * q1q2);
        float w1 = k * (q2Square * rq1 - q1q2 * rq2);
        float w2 = k * (-q1q2 * rq1 + q1Square * rq2);
        float w0 = 1 - w1 - w2;

        isInTriangle = w1 >= 0 && w2 >= 0 && w0 >= 0;

        //Debug.Log("w1 " + w1 + " w2 " + w2 + " w0 " + w0);

        return point;
    }

    public static Vector3 CalculateLineAndSphereIntersection(Line line, Sphere plane, ref bool isInSphere)
    {
        //二次多项式根的判别式 d * d = b * b - 4 * a * c，判断根的数量
        //p = s + vt
        float a = Dot(line.direction, line.direction);
        float b = 2 * Dot(line.start, line.direction);
        float c = Dot(line.start, line.start) - plane.radius * plane.radius;
        float d = b * b - 4 * a * c;

        Debug.Log("d " + d);


        if (d < 0)
        {
            isInSphere = false;
            return Vector3.zero;
        }
        else if (d > 0)
        {
            isInSphere = true;
            //两个交点 距离光线较近的一点
            float t1 = (- b - Mathf.Sqrt(d)) / (2 * a);
            float t2 = (-b + Mathf.Sqrt(d)) / (2 * a);
            Vector3 intersection = line.start + t1 * line.direction;
            return intersection;
        }
        else
        {
            isInSphere = true;
            float t = -b / (2 * a);
            Vector3 intersection = line.start + t * line.direction;
            return intersection;
        }
    }

    public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
	{
		return new Vector3 (lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
	}

	public static float Dot(Vector3 lhs, Vector3 rhs)
	{
		return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
	}

    // dist3D_Line_to_Line(): get the 3D minimum distance between 2 lines
    // Input:  two 3D lines L1 and L2
    // Return: the shortest distance between L1 and L2
    private static float dist3D_Line_to_Line(Line l1, Line l2)
    {
        Vector3 u = l1.end - l1.start;
        Vector3 v = l2.end - l2.start;
        Vector3 w = l1.start - l2.start;
        float a = Dot(u, u);         // always >= 0
        float b = Dot(u, v);
        float c = Dot(v, v);         // always >= 0
        float d = Dot(u, w);
        float e = Dot(v, w);
        float D = a * c - b * b;        // always >= 0
        float sc, tc;

        // compute the line parameters of the two closest points
        if (D < SMALL_NUM)
        {          // the lines are almost parallel
            sc = 0.0f;
            tc = (b > c ? d / b : e / c);    // use the largest denominator
        }
        else
        {
            sc = (b * e - c * d) / D;
            tc = (a * e - b * d) / D;
        }

        // get the difference of the two closest points
        Vector3 dP = w + (sc * u) - (tc * v);  // =  L1(sc) - L2(tc)

        return VectorLength(dP);   // return the closest distance
    }

    private static float VectorLength(Vector3 v)
    {
        float d = Dot(v, v);
        d = Mathf.Sqrt(d);
        return d;
    }

    // cpa_time(): compute the time of CPA for two tracks
    // Input:  two tracks Tr1 and Tr2
    // Return: the time at which the two tracks are closest
    private static float cpa_time(Line l1, Line l2)
    {
        Vector3 dv = l1.direction - l2.direction;

        float dv2 = Dot(dv, dv);
        if (dv2 < SMALL_NUM)      // the  tracks are almost parallel
        {
            return 0.0f;// any time is ok.  Use time 0.
        }

        Vector3 w0 = l1.start - l2.start;
        float cpatime = -Dot(w0, dv) / dv2;

        return cpatime;             // time of CPA
    }

    // cpa_distance(): compute the distance at CPA for two tracks
    // Input:  two tracks Tr1 and Tr2
    // Return: the distance for which the two tracks are closest
    private static float cpa_distance(Line l1, Line l2)
    {
        float ctime = cpa_time(l1, l2);
        Vector3 P1 = l1.start + (ctime * l1.direction);
        Vector3 P2 = l2.start + (ctime * l2.direction);
        Vector3 d = P1 - P2;
        return VectorLength(d);            // distance at CPA
    }
}
