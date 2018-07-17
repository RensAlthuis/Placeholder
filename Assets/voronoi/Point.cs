using System;
using System.Collections.Generic;
using UnityEngine;

public class Point
{

    public float x;
    public float y;

    public Point(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Boolean Equals(Point p)
    {
        return (Mathf.Approximately(x, p.x) && Mathf.Approximately(y, p.y));
    }

    public static implicit operator Point(Vector2 p)
    {
        return new Point(p.x, p.y);
    }

    public static implicit operator Point(Vector3 p)
    {
        return new Point(p.x, p.z);
    }

    public static implicit operator Vector2(Point p)
    {
        if (p == null) { return new Vector2(0, 0); }
        return new Vector2(p.x, p.y);
    }

    public static implicit operator Vector3(Point p)
    {
        if (p == null) { return new Vector3(0, 0, 0); }
        return new Vector3(p.x, 0, p.y);
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }
}
