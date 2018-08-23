using System;
using UnityEngine;

public class Edge : BeachLineNode
{

    public Point start;
    public Point dir;
    public Point end;

    private Point left, right;
    public Edge neighbour;

    public Edge(string name, Point start, Point left, Point right) : base(name)
    {
        this.start = start;
        dir = (new Vector2((left.x + right.x) / 2, (left.y + right.y) / 2) - start).normalized;
        this.left = left;
        this.right = right;

    }

    //Intersect based on formula: y = ax + b
    public Point Intersect(Edge other)
    {
        Matrix m = new Matrix(new float[,]
        {
            {dir.x, -other.dir.x, other.start.x - start.x},
            {dir.y, -other.dir.y, other.start.y - start.y}
        });

        m.Solve();

        return new Point(start.x + (dir.x) * m.mat[0,2], start.y + (dir.y) * m.mat[0,2]);
    }

    //Solve the equation for x
    public Point Solve(float x)
    {
        Matrix m = new Matrix(new float[,]{
            {dir.x, 0, x - start.x},
            {dir.y, 1, 0 - start.y}
        });
        m.Solve();
        return new Point(x, start.y + dir.y * m.mat[0, 2]);
    }

    private Boolean IsPointOnLine(Point p)
    {
        return false;
    }

    override public string ToString()
    {
        return name + " (" +
                LeftChild.ToString() + ") ( " +
                RightChild.ToString() + ") ";
    }

}
