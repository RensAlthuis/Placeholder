﻿using System.Collections;
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

	public override string ToString(){
		return "(" + x + ", " + y + ")";
	}

    public bool within(Vector2 low, Vector2 high) {
        if (x < low.x || x > high.x || y < low.y || y > high.y) return false;
        return true;
    }
}
