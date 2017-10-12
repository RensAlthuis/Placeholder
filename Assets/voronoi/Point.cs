using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point {
    public float x;
    public float y;

    public Point(float _x, float _y) {
        x = _x;
        y = _y;
    }

    public Vector2 toVector2() {
        return new Vector2(x, y);
    }

    public Vector3 toVector3() {
        return new Vector3(x, 0, y);
    }
}
