using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Edge {
    public Point end;
    public Point start;
    public Point direction;
    public Point left;
    public Point right;
    public Edge neighbour;
    public float f;
    public float g;

    /*
		Constructor of the class

		s		: pointer to start
		a		: pointer to left place
		b		: pointer to right place
	*/

    public Edge(Point s, Point a, Point b) {
        start = s;
        left = a;
        right = b;                               

        f = (b.x - a.x) / (a.y - b.y);
        g = s.y - f * s.x;
        direction = new Point(b.y - a.y, -(b.x - a.x));
    
    }
}
