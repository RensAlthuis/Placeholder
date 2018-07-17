using System;
using System.Collections.Generic;
using UnityEngine;

public class Generator
{

    public List<Edge> edges;
    public List<Point> points;
    public bool isSite;
    public BeachLineNode root;
    Queue queue;
    float sweepLine;
    Vector4 bound;
    private List<Point> pointSet;

    public Generator(List<Point> pointSet, Vector4 bound)
    {
        Reset(pointSet, bound);
    }

    public void Reset(List<Point> pointSet, Vector4 bound)
    {
        this.pointSet = pointSet;
        edges = new List<Edge>();
        points = new List<Point>();
        queue = new Queue();
        root = null;

        //build list of point events,
        foreach (Point v in pointSet)
        {
            queue.Enqueue(new Event(v.y, v, true));
        }
    }

    public void Calculate()
    {
        //clean up the environment
        Reset(pointSet, bound);

        //handle all point/circle events in loop
        while (!queue.IsEmpty())
        {
            Step();
        }
    }

    public float Step()
    {
        //Handle a single Event in the algorithm
        if (queue.IsEmpty()) return sweepLine;
        Event e = queue.Dequeue();
        sweepLine = e.y;
        isSite = e.isPointEvent;

        if (e.isPointEvent)
        {
            HandlePointEvent(e);
        } else
        {
            HandleCircleEvent(e);
        }
        return e.y;
    }

    public void ConnectNeighbours()
    {
        //connect all the edges that are extensions of eachother.
        foreach (Edge e in edges) {
            
            if (e.neighbour != null)
            {
                e.start = e.neighbour.end;
            }
        }
    }

    public void FinishEdges(BeachLineNode r)
    {
        //Absolutely HIDEOUS CODE that extends all exposed edges
        //to the edges of the bounding box
        //if (r.isParabola == false)
        //{
        //    Edge eCur = (Edge)r;

        //    if (eCur.start.x > lx && 
        //        eCur.start.x < rx && 
        //        eCur.start.y < ty && 
        //        eCur.start.y > by)
        //    {
        //        Point p = new Point(0, 0);
        //        if (eCur.left.x >= eCur.right.x)
        //        { //down
        //            if (eCur.isVertical)
        //            {
        //                p = new Point(eCur.b, by);
        //            } else
        //            {
        //                if (eCur.left.y > eCur.right.y)
        //                {
        //                    p = eCur.Solve(rx);
        //                } else
        //                {
        //                    p = eCur.Solve(lx);
        //                }
        //                if (p.y < by)
        //                {
        //                    p = eCur.SolveY(by);
        //                }
        //            }
        //        } else if (eCur.left.x < eCur.right.x)
        //        {//up
        //            if (eCur.isVertical)
        //            {
        //                p = new Point(eCur.b, ty);
        //            } else
        //            {
        //                if (eCur.left.y > eCur.right.y)
        //                {
        //                    p = eCur.Solve(rx);
        //                } else
        //                {
        //                    p = eCur.Solve(lx);
        //                }
        //                if (p.y > ty)
        //                {
        //                    p = eCur.SolveY(ty);
        //                }
        //            }
        //        }

        //        eCur.end = p;
        //        points.Add(p);
        //    }
        //    FinishEdges(r.LeftChild);
        //    FinishEdges(r.RightChild);
        //}
    }

    public void HandlePointEvent(Event e)
    {
        //Create new parabola
        Parabola p = new Parabola(e.point.ToString(), e.point);

        //If it is first parabola, simply add it to root and we are done
        if (root == null)
        {
            root = p;
            return;
        }

        //If the second parabola has the same y as the first,
        //we have a special case!
        if (root.isParabola && Mathf.Approximately(((Parabola)root).focus.y, e.y))
        {

            //Points of the first parabola
            float x = ((Parabola)root).focus.x;
            float y = ((Parabola)root).focus.y;

            //y of starting point of the new edge is actually -infinity, 
            //but bound by the bounding box.
            Point s = new Point((e.point.x + x) / 2, bound.z);
            Edge edge = new Edge("O", s, e.point, new Point(x, y))
            {
                LeftChild = p,
                RightChild = root
            };
            root = edge;

            edges.Add(edge);
            points.Add(s);

            //Done
            return;
        }

        //find parabola at e.x    
        Parabola p2 = ParabolaUnderX(e.point.x);

        //create the new parabolas
        Parabola pl = new Parabola(p2.focus.ToString(), p2.focus);
        Parabola pr = new Parabola(p2.focus.ToString(), p2.focus);

        //p2 will now have different neighbours, 
        //so the old circle event is invalid and can be deleted.
        queue.Delete(p2.circleEvent);
        p2.circleEvent = null;

        //Start point for the new edges
        Point start = new Point(e.point.x, p2.Solve(e.point.x, sweepLine));
        points.Add(start);

        //Create the new edges going in opposite directions from start
        Edge e1 = new Edge("O", start, p.focus, pl.focus)
        {
            RightChild = p,
            LeftChild = pl
        };

        Edge e2 = new Edge("O", start, pr.focus, p.focus)
        {
            LeftChild = e1,
            RightChild = pr
        };

        //give e1, e2 as a neighbour so we can connect them later
        e1.neighbour = e2;
        edges.Add(e1);
        //edges.Add(e2);

        //Update the beachline with the new edges
        if (p2.parent == null)
        {
            root = e2;
        } else
        {
            if (p2.parent.LeftChild == p2)
            {
                p2.parent.LeftChild = e2;
            } else
            {
                p2.parent.RightChild = e2;
            }
        }

        //check for possible circle events for pl and pr
        CheckCircle(pl);
        CheckCircle(pr);
    }

    public void HandleCircleEvent(Event e)
    {
        points.Add(e.point);

        //These edges are the edges that intersect during this event.
        Edge edgeLeft = e.par.GetLeftEdge();
        Edge edgeRight = e.par.GetRightEdge();
        if (edgeLeft == edgeRight)
        {
            return;
        }

        //The parabolas to the left and right of the parabola that 
        //is being removed.
        Parabola parLeft = e.par.LeftNeighbour();
        Parabola parRight = e.par.RightNeighbour();

        //the parabolas need to recalculate potential circle events at the end,
        //since they will now have different neighbours.
        if (parLeft.circleEvent != null)
        {
            queue.Delete(parLeft.circleEvent);
            parLeft.circleEvent = null;
        }

        if (parRight.circleEvent != null)
        {
            queue.Delete(parRight.circleEvent);
            parRight.circleEvent = null;
        }

        //The edges are finished at the intersect.
        edgeLeft.end = e.point;
        edgeRight.end = e.point;

        //The new edge that is created replaces the edgeLeft or edgeRight,
        //depending on which is highest in the tree
        Edge newEdge = new Edge("O", e.point, parRight.focus, parLeft.focus);

        edges.Add(newEdge);

        //find whether left or right is the highest edge
        BeachLineNode cur = e.par;
        Edge highestEdge = null;
        while (cur != root)
        {
            cur = cur.parent;
            if (cur == edgeLeft) highestEdge = (Edge)cur;
            if (cur == edgeRight) highestEdge = (Edge)cur;
        }
        newEdge.LeftChild = highestEdge.LeftChild;
        newEdge.RightChild = highestEdge.RightChild;

        //replace the highest edge with the new edge
        if (highestEdge.parent == null)
        {
            root = newEdge;
        } else
        {
            if (highestEdge.parent.LeftChild == highestEdge)
            {
                highestEdge.parent.LeftChild = newEdge;
            } else
            {
                highestEdge.parent.RightChild = newEdge;
            }
        }

        //remove the parabola from the beachline tree
        //we need to know which side the parabola is on relative to its parent
        //and then use that to fix the beachline
        BeachLineNode p = e.par.parent.parent;

        if (e.par.parent.LeftChild == e.par)
        {
            if (p.LeftChild == e.par.parent)
                p.LeftChild = e.par.parent.RightChild;
            if (p.RightChild == e.par.parent)
                p.RightChild = e.par.parent.RightChild;
        } else
        {
            if (p.LeftChild == e.par.parent)
                p.LeftChild = e.par.parent.LeftChild;
            if (p.RightChild == e.par.parent)
                p.RightChild = e.par.parent.LeftChild;
        }

        //Recalculating potential circle Events
        CheckCircle(parLeft);
        CheckCircle(parRight);
    }

    public Parabola ParabolaUnderX(float x)
    {
        BeachLineNode cur = root;

        //step down through edges until we find the right parabola
        while (!cur.isParabola)
        {
            //the parabolas to the left and right of this edge
            Parabola l = cur.LeftChild.RightPar();
            Parabola r = cur.RightChild.LeftPar();

            //the intersection, x1 and x2 of these two parabolas
            Point intersect = l.Intersect(r, sweepLine);

            //Magic math! 
            //but it chooses the right intersect point based on the y coordinate
            float a;
            if (!Mathf.Approximately(l.focus.y, r.focus.y) && l.focus.y < r.focus.y)
            {
                a = Math.Max(intersect.x, intersect.y);
            } else
            {
                a = Math.Min(intersect.x, intersect.y);
            }

            // a < x that means that the point x must be to the right of 
            //intersect a important is that if a = NaN (we divided by zero) 
            //a < x will not hold and we go left.
            // this is because we sort events with equal y on highest 
            //x coordinate instead of smallest x
            if (a < x && !Mathf.Approximately(a, x))
            {
                cur = cur.RightChild;
            } else
            {
                cur = cur.LeftChild;
            }
        }

        return (Parabola)cur;
    }

    public void CheckCircle(Parabola par)
    {
        //Get left and right edges
        Edge lEdge = par.GetLeftEdge();
        Edge rEdge = par.GetRightEdge();
        Parabola l = par.LeftNeighbour();
        Parabola r = par.RightNeighbour();
        if (l == null || r == null || l.focus.Equals(r.focus))
        {
            return;
        }

        //Intersect point will be the center of a circle event
        Point p = lEdge.Intersect(rEdge);
        if (p == null)
        {
            return;
        }
        
        //y of event will be at (center - circle radius),
        //using pythagoras theorem to find radius
        float dx = (par.focus.x - p.x);
        float dy = (par.focus.y - p.y);
        float radius = Mathf.Sqrt(dx * dx + dy * dy);
        float site = p.y - radius;

        //if (!isSite && (Mathf.Approximately(site, sweepLine) || site > sweepLine)) { return; }
        Event e = new Event(site , p, false);

        par.circleEvent = e;
        e.par = par;
        queue.Enqueue(e);
    }
}

