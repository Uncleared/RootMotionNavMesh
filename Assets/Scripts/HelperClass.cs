using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperClass
{

    public static Vector3 PickRandomPoint(Vector3 origin, float radius)
    {
        var point = Random.insideUnitSphere * radius;
        point.y = 0;
        point += origin;
        return point;
    }

    public static float TopDownDistance(Vector3 p1, Vector3 p2)
    {
        return Vector2.Distance(new Vector2(p1.x, p1.z), new Vector2(p2.x, p2.z));
    }
}
