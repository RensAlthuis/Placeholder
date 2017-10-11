using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voronoi {

    Parabola root = null;
    float s;
    EventQueue events;
    float width;
    float height;

    List<Edge> edges;

    public Voronoi() {
        events = new EventQueue();
        edges = new List<Edge>();
    }

    public List<Edge> GetEdges(List<Vector2> vecs, float w, float h) {

        width = w;
        height = h;

        edges.Clear();
        //initialze an event queue with all site events

        foreach (Vector2 v in vecs) {
            events.Add(new Event(v, true));
        }


        while (!events.isEmpty()) {
            Event e = events.Remove();
            s = e.y;
            if (e.isPlaceEvent) {
                handleSiteEvent(e);
            } else {
                handleCircleEvent(e);
            }
        }

        FinishEdge(root);

        foreach (Edge e in edges) {
            if(e.neighbour != null) {
                e.start = e.neighbour.end;
            }
        }

        return edges;
    }

    private void handleSiteEvent(Event e) {

        //No parabolas yet
        if (root == null) {
            root = new Parabola(ref e.point);
            return;
        }

        if (root.isLeaf && root.site.y - e.point.y < 1) // edge case, both lowest position are at the same height
{
            Vector2 fp = root.site;
            root.isLeaf = false;
            root.SetLeft(new Parabola(ref fp));
            root.SetRight(new Parabola(ref e.point));
            Vector2 s = new Vector2((e.point.x + fp.x) / 2, height); // new edge is in center of 2 sites starting at screen height.
            //points.push_back(s);
            if (e.point.x > fp.x) root.edge = new Edge(ref s, ref fp, ref e.point); // decide which parabola is left or right
            else root.edge = new Edge(ref s, ref e.point, ref fp);
            edges.Add(root.edge);
            return;
        }

        Parabola par = getParabolaByX(e.point.x);

        //If par has a circle event, it will never fire and we can delete it.
        if (par.cEvent != null) {
            par.cEvent = null;
        }

        Vector2 start = new Vector2(e.point.x, getY(ref par.site, e.point.x));
        //points.push_back(start);

        Edge el = new Edge(ref start, ref par.site, ref e.point); // edge going from e.point.x left
        Edge er = new Edge(ref start, ref e.point, ref par.site); // edge going from e.point.x right

        el.neighbour = er;
        edges.Add(el);
        //current parabola becomes an edge
        par.edge = er;
        par.isLeaf = false;


        Parabola p0 = new Parabola(ref par.site); // left sub-parabola
        Parabola p1 = new Parabola(ref e.point); // newly created parabola
        Parabola p2 = new Parabola(ref par.site); // right sub-parabola

        //left is the edge going left
        par.SetLeft(new Parabola());
        par.Left().edge = el;

        //right is the right sub-parabola
        par.SetRight(p2);

        //left break in to 2 new parabolas, the left sub-parabola and the new full parabola
        par.Left().SetLeft(p0);
        par.Left().SetRight(p1);


        //check for new potential circle events at the 2 sub parabolas
        CheckCircle(p0);
        CheckCircle(p2);
    }

    private void handleCircleEvent(Event e) {
        Parabola p1 = e.arch;

        Parabola xl = p1.GetLeftParent();
        Parabola xr = p1.GetRightParent();

        Parabola p0 = xl.GetLeftChild();
        Parabola p2 = xr.GetRightChild();

        // we are handling the circle events in p0 and p2 right now and can remove them.
        if (p0.cEvent != null) p0.cEvent = null;
        if (p2.cEvent != null) p2.cEvent = null;

        Vector2 p = new Vector2(e.point.x, getY(ref p1.site, e.point.x));
        //points.push_back(p);

        // finish the edges
        xl.edge.end = p;
        xr.edge.end = p;

        Parabola higher = xl;
        Parabola par = p1;
        while(par != root) {
            par = par.parent;
            if (par == xl) higher = xl;
            if (par == xr) higher = xr;
        }

        higher.edge = new Edge(ref p, ref p0.site, ref p2.site);
        edges.Add(higher.edge);

        Parabola gparent = p1.parent.parent;
        if(p1.parent.Left() == p1) {
            if (gparent.Left() == p1.parent) gparent.SetLeft(p1.parent.Right());
            if (gparent.Right() == p1.parent) gparent.SetRight(p1.parent.Right());
        } else {
            if (gparent.Left() == p1.parent) gparent.SetLeft(p1.parent.Left());
            if (gparent.Right() == p1.parent) gparent.SetRight(p1.parent.Left());
        }

        CheckCircle(p0);
        CheckCircle(p2);
    }

    private void FinishEdge(Parabola n) {
        if (n.isLeaf) { n = null;  return; }
        float mx;
        if (n.edge.direction.x > 0.0f) mx = Mathf.Max(width, n.edge.start.x + 10);
        else mx = Mathf.Min(0.0f, n.edge.start.x - 10);

        Vector2 end = new Vector2(mx, mx * n.edge.f + n.edge.g);
        n.edge.end = end;
        //points.push_back(end);

        FinishEdge(n.Left());
        FinishEdge(n.Right());
        n = null;
    }

    private float getXOfEdge(Parabola par, float y) {
        Parabola left = par.GetLeftChild();
        Parabola right = par.GetRightChild();


        Vector2 p = left.site;
        Vector2 r = right.site;

        float dp = 2.0f * (p.y - y);
        float a1 = 1.0f / dp;
        float b1 = -2.0f * p.x / dp;
        float c1 = y + dp / 4 + p.x * p.x / dp;

        dp = 2.0f * (r.y - y);
        float a2 = 1.0f / dp;
        float b2 = -2.0f * r.x / dp;
        float c2 = s + (dp / 4) + ((r.x * r.x) / dp);

        float a = a1 - a2;
        float b = b1 - b2;
        float c = c1 - c2;

        // Quadratic formula
        float disc = b * b - 4 * a * c;
        float x1 = (-b + Mathf.Sqrt((float)disc)) / (2.0f * a);
        float x2 = (-b - Mathf.Sqrt((float)disc)) / (2.0f * a);

        float ry;
        if (p.y < r.y) ry = Mathf.Max(x1, x2);
        else ry = Mathf.Min(x1, x2);

        return ry;
    }

    private Parabola getParabolaByX(float xx) {
        Parabola par = root;
        float x = 0.0f;

        while (!par.isLeaf)
        {
            x = getXOfEdge(par, s);
            if (x > xx) par = par.Left();
            else par = par.Right();
        }
        return par;
    }

    private float getY(ref Vector2 p, float x)
    {
        float dp = 2 * (p.y - s);
        float a1 = 1 / dp;
        float b1 = -2 * p.x / dp;
        float c1 = s + dp / 4 + p.x * p.x / dp;

        return (a1 * x * x + b1 * x + c1);
    }

    private void CheckCircle(Parabola b) {

        Parabola lp = b.GetLeftParent();
        Parabola rp = b.GetRightParent();

        if (lp == null || rp == null) return;
        Parabola a = lp.GetLeftChild();
        Parabola c = rp.GetRightChild();

        if (a == null || c == null || a.site == c.site) return;

        Vector2? v = null;
        v = GetEdgeIntersection(lp.edge, rp.edge);
        if (v == null) return;

        float dx = a.site.x - v.GetValueOrDefault().x;
        float dy = a.site.y - v.GetValueOrDefault().y;

        float d = Mathf.Sqrt((dx * dx) + (dy * dy));

        if (v.GetValueOrDefault().y - d >= s) { return; }

        Event e = new Event(new Vector2(v.GetValueOrDefault().x, v.GetValueOrDefault().y - d), false);
        //points.push_back(e.point);
        b.cEvent = e;
        e.arch = b;
        events.Add(e);
    }

    Vector2? GetEdgeIntersection(Edge a, Edge b) {
        float x = (b.g - a.g) / (a.f - b.f);
        float y = a.f * x + a.g;

        if ((x - a.start.x) / a.direction.x < 0) return null;
        if ((y - a.start.y) / a.direction.y < 0) return null;

        if ((x - b.start.x) / b.direction.x < 0) return null;
        if ((y - b.start.y) / b.direction.y < 0) return null;

        Vector2? p = new Vector2(x, y);
        //points.push_back(p);
        return p;
    }
}
