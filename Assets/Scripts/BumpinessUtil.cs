using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Nash
/// </summary>
public class BumpinessUtil
{
    Vector4 vertexPosition;
    Vector3 normal;
    Vector4 tangent;
    Vector3 view;
    Vector3 light;
    Vector4[] mvpMatrix;
    Vector3 cameraPosition;
    Vector3 lightDirection;
    // Use this for initialization
    void Mian () {
        //Transform the vertex into clip space.
        Vector3 glPosition = new Vector4(Vector4.Dot(mvpMatrix[0], vertexPosition),
                                        Vector4.Dot(mvpMatrix[1], vertexPosition),
                                        Vector4.Dot(mvpMatrix[2], vertexPosition),
                                        Vector4.Dot(mvpMatrix[3], vertexPosition));
        //Calculate the bitangent
        Vector3 bitangent = Vector3.Cross(normal, new Vector3(tangent.x, tangent.y, tangent.z)) * tangent.w;

        //Transform V into tangent space.
        view = cameraPosition - new Vector3(vertexPosition.x, vertexPosition.y, vertexPosition.z);
        view = new Vector3(Vector4.Dot(tangent, new Vector4(view.x, view.y, view.z, 0)), 
                           Vector4.Dot(bitangent, new Vector4(view.x, view.y, view.z, 0)),
                           Vector4.Dot(normal, new Vector4(view.x, view.y, view.z, 0)));

        //Transform L into tangent space.
        light = new Vector3(Vector4.Dot(tangent, new Vector4(lightDirection.x, lightDirection.y, lightDirection.z, 0)),
                            Vector4.Dot(bitangent, new Vector4(lightDirection.x, lightDirection.y, lightDirection.z, 0)),
                            Vector4.Dot(normal, new Vector4(lightDirection.x, lightDirection.y, lightDirection.z, 0)));
    }
}
