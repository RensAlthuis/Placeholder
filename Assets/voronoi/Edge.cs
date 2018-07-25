using System;
using UnityEngine;

public class Edge : BeachLineNode
{

    
    public Point start, end;
    public Point left, right;
    public float dx, dy;
    public float a, b;
    public Edge neighbour;
    public Boolean isVertical;

    public Edge(string name, Point start, Point left, Point right) : base(name)
    {

        this.start = start;
        this.left = left;
        this.right = right;
        this.isParabola = false;
        isVertical = Mathf.Approximately(this.left.y,this.right.y);
        if (isVertical)
        {
            a = 0; // kinda should't be 0 but NaN
            b = start.x;
        } else
        {
            a = (this.right.x - this.left.x) / (this.left.y - this.right.y);
            b = this.start.y - a * this.start.x;
        }
    }

    //Intersect based on formula: y = ax + b
    public Point Intersect(Edge other)
    {

        //parallel lines
        if (isVertical && other.isVertical)
            return null;
        if (Mathf.Approximately(a, other.a))
            if (!isVertical && !other.isVertical)
                return null;

        Point p;

        if (isVertical)
        {
            p = other.Solve(b);
        } else if (other.isVertical)
        {
            p = Solve(other.b);
        } else
        {
            p = Solve((other.b - b) / (a - other.a));
        }
        if (p!= null)
            if (!IsPointOnLine(p) || !other.IsPointOnLine(p))
                return null;
        return p;
    }

    //Solve the equation for x
    public Point Solve(float x)
    {
        if (isVertical) return null;
        Point p = new Point(x , a * x + b);
        if (!IsPointOnLine(p)) return null;
        return p;
    }

    private Boolean IsPointOnLine(Point p)
    {
        if (Mathf.Approximately(left.y, right.y))
        {
            if (!Mathf.Approximately(left.x, right.x) && left.x > right.x)
                if (!Mathf.Approximately(p.y, start.y) && p.y > start.y)
                    return false;
            if (!Mathf.Approximately(left.x, right.x) && left.x < right.x)
                if (!Mathf.Approximately(p.y, start.y) && p.y < start.y)
                    return false;
            return true;
        }

        if (left.y > right.y)
            return !(!Mathf.Approximately(p.x, start.x) && p.x < start.x);

        if (left.y < right.y)
            return !(!Mathf.Approximately(p.x, start.x) && p.x > start.x);

        return true;
    }

    override public string ToString()
    {
        return name + " (" +
                LeftChild.ToString() + ") ( " +
                RightChild.ToString() + ") ";
    }

}
