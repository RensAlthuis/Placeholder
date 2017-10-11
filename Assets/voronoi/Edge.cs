using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Edge {
    public Vector2 end;
    public Vector2 start;
    public Vector2 direction;
    public Vector2 left;
    public Vector2 right;
    public Edge neighbour;
    public float f;
    public float g;

    /*
		Constructor of the class

		s		: pointer to start
		a		: pointer to left place
		b		: pointer to right place
	*/

    public Edge(ref Vector2 s, ref Vector2 a, ref Vector2 b) {
        start = s;
        left = a;
        right = b;

        f = (b.x - a.x) / (a.y - b.y);
        g = s.y - f * s.x;
        direction = new Vector2(b.y - a.y, -(b.x - a.x));
    }
}
