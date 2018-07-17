using System;
using UnityEngine;

public class Parabola : BeachLineNode
{

    public Point focus;
    public Event circleEvent;

    public Parabola(string name, Point focus) : base(name)
    {
        this.focus = focus;
        this.isParabola = true;
    }

    //step up till we go to the left, then step down and go right till we hit a leaf
    public Parabola LeftNeighbour()
    {
        BeachLineNode cur = this;

        if (cur.parent == null)
            return null;

        while (cur.parent.LeftChild == cur)
        {
            cur = cur.parent;
            if (cur.parent == null)
                return null;
        }

        cur = cur.parent.LeftChild;
        while (cur.RightChild != null)
        {
            cur = cur.RightChild;
        }

        return (Parabola)cur;
    }

    //step up till we go to the right, then step down and go left till we hit a leaf
    public Parabola RightNeighbour()
    {
        BeachLineNode cur = this;
        if (cur.parent == null) { return null; }

        while (cur.parent.RightChild == cur)
        {
            cur = cur.parent;
            if (cur.parent == null) { return null; }
        }

        cur = cur.parent.RightChild;
        while (cur.LeftChild != null)
        {
            cur = cur.LeftChild;
        }

        return (Parabola)cur;
    }

    override public string ToString()
    {
        return " " + name + " ";
    }

    public float Solve(float x, float d)
    {
        if (Mathf.Approximately(focus.y, d)){
            return focus.y;
        }
        float k = (2 * (focus.y - d));
        float a = 1 / k;
        float b = -(2 * focus.x) / k;
        float c = (focus.y * focus.y - d * d + focus.x * focus.x) / k;

        return (a * (x * x) + b * x + c);
    }

    public Point Intersect(Parabola par, float d)
    {
        //                          1               2x            y^2 - x^2 - d^2
        //Formula for parabala: --------- n^2 - ----------- n +  ------------------
        //                       2(y - d)        2(y - d)            2(y - d)

        if(Mathf.Approximately(focus.y, d))
        {
            return new Point(focus.x, par.Solve(focus.x, d));
        }

        if(Mathf.Approximately(par.focus.y, d))
        {
            return new Point(par.focus.x, Solve(par.focus.x, d));
        }

        float k = (2 * (focus.y - d));
        float a1 = 1 / k;
        float b1 = -(2 * focus.x) / k;
        float c1 = (focus.y * focus.y - d * d + focus.x * focus.x) / k;

        k = (2 * (par.focus.y - d));
        float a2 = 1 / k;
        float b2 = -(2 * par.focus.x) / k;
        float c2 = (par.focus.y * par.focus.y - d * d + par.focus.x * par.focus.x) / k;

        //par1 = par2 -> par1 - par2 = 0
        float a = a1 - a2;
        float b = b1 - b2;
        float c = c1 - c2;

        if (Mathf.Approximately(a, 0))
        {
            if (Mathf.Approximately(b1, b2)) Debug.Log("nooooooooo");
            float x = (c2 - c1) / (b1 - b2);
            return new Point(x, x);
        }

        //Quadratic formula
        float det = b * b - 4 * a * c;
        if (det < 0 || a == 0) Debug.Log("nooooooo");
        float x1 = (-b + (Mathf.Sqrt(det))) / (2 * a);
        float x2 = (-b - (Mathf.Sqrt(det))) / (2 * a);

        return new Point(x1, x2);
    }

    public Edge GetLeftEdge()
    {
        BeachLineNode cur = this;
        if (cur.parent == null) return null;
        while (cur.parent.LeftChild == cur)
        {
            cur = cur.parent;
            if (cur.parent == null) return null;
        }
        return (Edge)cur.parent;
    }

    public Edge GetRightEdge()
    {
        BeachLineNode cur = this;
        if (cur.parent == null) return null;
        while (cur.parent.RightChild == cur)
        {
            cur = cur.parent;
            if (cur.parent == null) return null;
        }
        return (Edge)cur.parent;
    }

}
