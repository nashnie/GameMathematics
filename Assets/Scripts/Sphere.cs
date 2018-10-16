using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sphere
/// </summary>
public class Sphere : MonoBehaviour
{
    //x * x + y * y + z * z = r * r
    public Point center;
    public float radius;

    private Vector3 lineAndSphereIntersection;

    public Vector3 LineAndSphereIntersection
    {
        set
        {
            lineAndSphereIntersection = value;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = lineAndSphereIntersection;
            cube.transform.localScale = Vector3.one * 0.1f;
        }
    }

    // Use this for initialization
    void Start () {
        center.location = transform.position;
    }
	
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(center.location, radius);
    }
}
