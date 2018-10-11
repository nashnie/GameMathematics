using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Plane
{
    public Point p0;
    public Point p1;
    public Point p2;

    protected override void Awake()
    {
        planeNormal = GameMathematicsStatics.Cross(p1.location - p0.location, p2.location - p0.location).normalized;
        planeCenter = transform.position;
    }
}
