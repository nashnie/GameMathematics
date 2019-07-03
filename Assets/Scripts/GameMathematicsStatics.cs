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

    //Newton's Method Sqrt
    public static double Sqrt(double a)
    {
        if (a < 0)
        {
            throw new System.Exception("Can not sqrt a negative number.");
        }
        double error = 0.00001;
        double x = 1;
        while (true)
        {
            double val = x * x;
            if (System.Math.Abs(val - a) <= error)
            {
                return x;
            }
            x = x / 2 + a / (2 * x);
        }
    }


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

    //https://blog.csdn.net/xoyojank/article/details/54030418
    //https://blog.uwa4d.com/archives/UWALab_NVIDIAGameWorks.html
    //先实现任意平面切割，再实现根据不同类型算法生成的不同碎片
    public static void SliceMesh(Plane plane, Transform meshTransform)
    {      
        Vector3 planeCenter = plane.planeCenter;
        Vector3 planeNormal = plane.planeNormal;
        Vector3 localPlaneCenter = meshTransform.InverseTransformPoint(planeCenter);
        Vector3 localPlaneNormal = meshTransform.InverseTransformVector(planeNormal);
        localPlaneNormal = localPlaneNormal.normalized;
        plane.w = Dot(localPlaneCenter, localPlaneNormal);
        Debug.DrawLine(localPlaneCenter, localPlaneCenter + localPlaneNormal * 10.0f, Color.green);
        BoxCollider boxCollider = meshTransform.GetComponent<BoxCollider>();
        int boxPlaneCompare = BoxPlaneCompare(boxCollider, localPlaneNormal, plane.w);
        if (boxPlaneCompare == -1)
        {
            Debug.Log("boxPlaneCompare left");
        }
        else if (boxPlaneCompare == 1)
        {
            Debug.Log("boxPlaneCompare right");
        }
        else
        {
            Debug.Log("boxPlaneCompare intersect");
            Dictionary<int, int> baseToSlicedVertIndex = new Dictionary<int, int>();
            Dictionary<int, int> baseToOtherSlicedVertIndex = new Dictionary<int, int>();
            List<Vector3> slicedVerts = new List<Vector3>();
            List<int> slicedIndexs = new List<int>();
            List<Vector3> otherSlicedVerts = new List<Vector3>();
            List<int> otherSlicedIndexs = new List<int>();
            MeshFilter meshFilter = meshTransform.GetComponent<MeshFilter>();
            if (meshFilter)
            {
                int baseVertsNum = meshFilter.mesh.vertexCount;
                int baseIndexsNum = meshFilter.mesh.triangles.Length;
                float[] vertDist = new float[baseVertsNum];
                for (int baseVertIndex = 0; baseVertIndex < baseVertsNum; baseVertIndex++)
                {
                    Vector3 vert = meshFilter.mesh.vertices[baseVertIndex];
                    float boxCenterDist = PlaneDot(planeNormal, plane.w, vert);
                    vertDist[baseVertIndex] = boxCenterDist;
                    int slicedVertIndex;
                    if (vertDist[baseVertIndex] > 0.0f)
                    {
                        slicedVertIndex = slicedVerts.Count;
                        slicedVerts.Add(vert);
                        baseToSlicedVertIndex.Add(baseVertIndex, slicedVertIndex);
                    }
                    else
                    {
                        slicedVertIndex = slicedVerts.Count;
                        otherSlicedVerts.Add(vert);
                        baseToOtherSlicedVertIndex.Add(baseVertIndex, slicedVertIndex);
                    }
                }

                for (int baseIndex = 0; baseIndex < baseIndexsNum; baseIndex += 3)//Triangles
                {
                    int[] baseVert = new int[3];
                    int[] slicedVert = new int[3];
                    int[] otherSlicedVert = new int[3];

                    for (int i = 0; i < 3; i++)
                    {
                        baseVert[i] = baseIndex + i;
                        if (baseToSlicedVertIndex.ContainsKey(baseIndex + i))
                        {
                            slicedVert[i] = baseToSlicedVertIndex[baseIndex + i];
                        }
                        else
                        {
                            slicedVert[i] = -1;
                        }
                        if (baseToOtherSlicedVertIndex.ContainsKey(baseIndex + i))
                        {
                            otherSlicedVert[i] = baseToOtherSlicedVertIndex[baseIndex + i];
                        }
                        else
                        {
                            otherSlicedVert[i] = -1;
                        }
                    }

                    if (slicedVert[0] != -1 && slicedVert[1] != -1 && slicedVert[2] != -1)
                    {
                        slicedIndexs.Add(slicedVert[0]);
                        slicedIndexs.Add(slicedVert[1]);
                        slicedIndexs.Add(slicedVert[2]);
                    }
                    else if (otherSlicedVert[0] != -1 && otherSlicedVert[1] != -1 && otherSlicedVert[2] != -1)
                    {
                        otherSlicedIndexs.Add(otherSlicedVert[0]);
                        otherSlicedIndexs.Add(otherSlicedVert[1]);
                        otherSlicedIndexs.Add(otherSlicedVert[2]);
                    }
                    else
                    {
                        int[] finalVerts = new int[4];
                        int finalVertsNum = 0;

                        int[] otherFinalVerts = new int[4];
                        int otherFinalVertsNum = 0;

                        float[] planeDist = new float[3];
                        planeDist[0] = vertDist[0];
                        planeDist[1] = vertDist[1];
                        planeDist[2] = vertDist[2];

                        for (int edgeIndex = 0; edgeIndex < 3; edgeIndex++)
                        {
                            int thisVert = edgeIndex;
                            if (slicedVert[thisVert] != -1)
                            {
                                finalVerts[finalVertsNum++] = slicedVert[thisVert];
                            }
                            else
                            {
                                otherFinalVerts[otherFinalVertsNum++] = otherSlicedVert[thisVert];
                            }

                            int nextVert = (edgeIndex + 1) % 3;
                            bool bSlicedThisVert = slicedVert[edgeIndex] == -1;
                            bool bSlicedNextVert = slicedVert[nextVert] == -1;
                            if (bSlicedThisVert != bSlicedNextVert)
                            {
                                float alpha = -planeDist[thisVert] / (planeDist[nextVert] - planeDist[thisVert]);
                                Vector3 interpVert = Vector3.Lerp(meshFilter.mesh.vertices[thisVert], meshFilter.mesh.vertices[nextVert], alpha);

                                finalVerts[finalVertsNum++] = slicedVerts.Count;
                                otherFinalVerts[otherFinalVertsNum++] = otherSlicedVerts.Count;

                                slicedVerts.Add(interpVert);
                                otherSlicedVerts.Add(interpVert);
                            }
                        }

                        for (int vertIndex = 2; vertIndex < finalVertsNum; vertIndex++)
                        {
                            slicedIndexs.Add(finalVerts[0]);
                            slicedIndexs.Add(finalVerts[vertIndex - 1]);
                            slicedIndexs.Add(finalVerts[vertIndex]);
                        }

                        for (int vertIndex = 2; vertIndex < otherFinalVertsNum; vertIndex++)
                        {
                            otherSlicedIndexs.Add(otherFinalVerts[0]);
                            otherSlicedIndexs.Add(otherFinalVerts[vertIndex - 1]);
                            otherSlicedIndexs.Add(otherFinalVerts[vertIndex]);
                        }
                    }
                }
            }

            Mesh newSlicedMesh = new Mesh();
            Mesh newOtherSlicedMesh = new Mesh();
            newSlicedMesh.vertices = slicedVerts.ToArray();
            newSlicedMesh.triangles = slicedIndexs.ToArray();
            newOtherSlicedMesh.vertices = otherSlicedVerts.ToArray();
            newOtherSlicedMesh.triangles = otherSlicedIndexs.ToArray();


            GameObject slicedMeshObject = new GameObject("SlicedMesh");
            GameObject otherSlicedMeshObject = new GameObject("OtherSlicedMesh");

            MeshFilter slicedMeshFilter = slicedMeshObject.AddComponent<MeshFilter>();
            MeshFilter otherSlicedmeshFilter = otherSlicedMeshObject.AddComponent<MeshFilter>();

            slicedMeshFilter.mesh = newSlicedMesh;
            otherSlicedmeshFilter.mesh = newOtherSlicedMesh;
        }
    }

    private static int BoxPlaneCompare(BoxCollider box, Vector3 planeNormal, float w)
    {
        Vector3 boxCenter = box.center + box.transform.position;
        Vector3 boxExtents = box.size;
        boxExtents.x *= box.transform.localScale.x;
        boxExtents.y *= box.transform.localScale.y;
        boxExtents.z *= box.transform.localScale.z;
        boxExtents /= 2;

        float boxCenterDist = PlaneDot(planeNormal, w, boxCenter);
        float boxSize = BoxPushOut(planeNormal, boxExtents);
        if (boxCenterDist > boxSize)
        {
            return 1;
        }
        else if (boxCenterDist < -boxSize)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private static float PlaneDot(Vector3 planeNormal, float w, Vector3 point)
    {
        return planeNormal.x * point.x + planeNormal.y * point.y + planeNormal.z * point.z - w;
    }

    private static float BoxPushOut(Vector3 planeNormal, Vector3 boxExtents)
    {
        float x = Mathf.Abs(boxExtents.x * planeNormal.x);
        float y = Mathf.Abs(boxExtents.y * planeNormal.y);
        float z = Mathf.Abs(boxExtents.z * planeNormal.z);

        return x + y + z;
    }
}
