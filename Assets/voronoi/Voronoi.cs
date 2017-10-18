using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Voronoi {

    List<Edge> edges;
    List<Event> deleted;
    EventQueue events;

    Parabola root;
    float s;
    float width;
    float height;
 

    public Voronoi() {
        events = new EventQueue();
        edges = new List<Edge>();
        deleted = new List<Event>();
    }

    public List<Edge> GetEdges(List<Point> vecs, float w, float h) {

        // places = vecs;
        width = w;
        height = h;
        root = null;

        edges.Clear();

        // initialize an event queue with all site events
        foreach (Point v in vecs) {
            events.Add(new Event(v, true));
        }


        while (!events.isEmpty()) {
            Event e = events.Remove();
            s = e.point.y;

            if (deleted.Contains(e)){
                deleted.Remove(e);
                continue;
            }

            if (e.isPlaceEvent) {
				//Debug.Log("site: " + e.point.x + ", " + e.point.y);
                handleSiteEvent(e);
            } else {
				//Debug.Log("circle: " + e.point.x + ", " + e.point.y);
                handleCircleEvent(e);
            }

        }

        FinishEdge(root);
        foreach (Edge e in edges) {

            if (e.neighbour != null) {
                e.start = e.neighbour.end;
                e.neighbour = null;
            }
        }

        return edges;
    }

    private void handleSiteEvent(Event e) {

        // No parabolas yet
        if (root == null) {
            Debug.Log("root == nul");
			root = new Parabola(e.point);
            return;
        }

        if (root.isLeaf && root.site.y - e.point.y <= 0){ // edge case, both lowest position are at the same height
            Debug.Log("HOLY SHIT THIS HAPPEND");

            Point fp = root.site;
            root.isLeaf = false;
            root.SetLeft(new Parabola(fp));
            root.SetRight(new Parabola(e.point));
            Point s = new Point((e.point.x + fp.x) / 2, height); // new edge is in center of 2 sites starting at screen height.
            if (e.point.x > fp.x) root.edge = new Edge(s, fp, e.point); // decide which parabola is left or right
            else root.edge = new Edge(s, e.point, fp);
            edges.Add(root.edge);
            return;
        }

        Parabola par = getParabolaByX(e.point.x);

        // If par has a circle event, it will never fire and we can delete it.
        if (par.cEvent != null) {
            deleted.Add(par.cEvent);
			//Debug.Log ("remove cEvent at: " + par.cEvent.point.x + ", " + par.cEvent.point.y);
            par.cEvent = null;

        }

        Point start = new Point(e.point.x, getY(par.site, e.point.x));
        Debug.Log(start + "" + par.site);
        Edge el = new Edge(start, par.site, e.point); // edge going from e.point.x left
        Edge er = new Edge(start, e.point, par.site); // edge going from e.point.x right

        el.neighbour = er;
        edges.Add(el);

        // current parabola becomes right-edge
        par.edge = er;
        par.isLeaf = false;


        Parabola p0 = new Parabola(par.site); // left sub-parabola
        Parabola p1 = new Parabola(e.point); // newly created parabola
        Parabola p2 = new Parabola(par.site); // right sub-parabola

        // left becomes left-edge
        par.SetLeft(new Parabola());
        par.Left().edge = el;

        // right is the right sub-parabola
        par.SetRight(p2);

        // left break in to 2 new parabolas, the left sub-parabola and the new full parabola
        par.Left().SetLeft(p0);
        par.Left().SetRight(p1);


        // check for new potential circle events at the 2 sub parabolas
        CheckCircle(p0);
        CheckCircle(p2);
    }

    private void handleCircleEvent(Event e) {

        // The center parabola (has just been eclipsed by two other parabolas)
        Parabola p1 = e.arch;

        // the left and right edges that join together in p1.site
        Parabola xl = p1.GetLeftParent();
        Parabola xr = p1.GetRightParent();

        // the left and right parabolas
        Parabola p0 = xl.GetLeftChild();
        Parabola p2 = xr.GetRightChild();

        // handling the circle events in p0 and p2 right now and can remove them.
        if (p0.cEvent != null) {
            deleted.Add(p0.cEvent);
            p0.cEvent = null;
        }
        if (p2.cEvent != null) {
            deleted.Add(p2.cEvent);
            p2.cEvent = null;
        }

        Point p = new Point(e.point.x, getY(p1.site, e.point.x)); // Point where p1 is eclipsed by p0 and p3 == intersect of edges xl and xr
		//Debug.Log ("new cpoint at: " + p + " from points: " + p0.site + " and " + p2.site);

        // finish the edges
		xl.edge.end = p;
		xr.edge.end = p;

        //create the new edge start from p
        Parabola higher = null;
        Parabola par = p1;
        while(par != root) {
            par = par.parent;
            if (par == xl) higher = xl;
            if (par == xr) higher = xr;
        }

        higher.edge = new Edge(p, p0.site, p2.site);
        edges.Add(higher.edge);

        //remove p1 from the beachline
        Parabola gparent = p1.parent.parent;
        if(p1.parent.Left() == p1) {
            if (gparent.Left() == p1.parent) gparent.SetLeft(p1.parent.Right());
            if (gparent.Right() == p1.parent) gparent.SetRight(p1.parent.Right());
        } else {
            if (gparent.Left() == p1.parent) gparent.SetLeft(p1.parent.Left());
            if (gparent.Right() == p1.parent) gparent.SetRight(p1.parent.Left());
        }

        //check for new potential circle events
        CheckCircle(p0);
        CheckCircle(p2);
    }

    private void FinishEdge(Parabola n) {
        if (n.isLeaf) { return; }
        float mx;
        if (n.edge.direction.x > 0.0f) {
            mx = width;
        } else {
			mx = 0.0f;
        }

        Point end = new Point(mx, mx * n.edge.f + n.edge.g);

        Debug.Log("start = " + n.edge.start + " left = " + n.edge.left + " right = " + n.edge.right + " dir = " + n.edge.direction.x + " end = " + end);
        //n.edge.end = end;

        FinishEdge(n.Left());
        FinishEdge(n.Right());
    }

    private float getXOfEdge(Parabola par, float y) {

        //get left and right parabola
        Parabola left = par.GetLeftChild();
        Parabola right = par.GetRightChild();

        //center points to left and right of edge
        Point p = left.site;
        Point r = right.site;

        //formula for left parabola
        float dp = 2.0f * (p.y - y);
        float a1 = 1.0f / dp;
        float b1 = -2.0f * p.x / dp;
        float c1 = y + dp / 4 + p.x * p.x / dp;

        // formula for right parabola
        dp = 2.0f * (r.y - y);
        float a2 = 1.0f / dp;
        float b2 = -2.0f * r.x / dp;
        float c2 = s + (dp / 4) + ((r.x * r.x) / dp);

        //calculate y1 = y2 (by doing y1-y2 = 0, and then using quadratic formula)
        float a = a1 - a2;
        float b = b1 - b2;
        float c = c1 - c2;

        // Quadratic formula
        float disc = b * b - 4 * a * c;
        float x1 = (-b + Mathf.Sqrt(disc)) / (2.0f * a);
        float x2 = (-b - Mathf.Sqrt(disc)) / (2.0f * a);

        if (p.y < r.y) return Mathf.Max(x1, x2);
        return Mathf.Min(x1, x2);
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

    private float getY(Point p, float x)
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

        Point v = null;
        v = GetEdgeIntersection(lp.edge, rp.edge);
        if (v == null) return;

        float dx = a.site.x - v.x;
        float dy = a.site.y - v.y;

        float d = Mathf.Sqrt((dx * dx) + (dy * dy));

        if (v.y - d >= s) { return; }

        Event e = new Event(new Point(v.x, v.y - d), false);
        b.cEvent = e;
        e.arch = b;
        events.Add(e);
		//Debug.Log ("new cEvent at: " + e.point.x + ", " + e.point.y);
    }

    Point GetEdgeIntersection(Edge a, Edge b) {
        float x = (b.g - a.g) / (a.f - b.f);
        float y = a.f * x + a.g;

        if ((x - a.start.x) / a.direction.x < 0) return null;
        if ((y - a.start.y) / a.direction.y < 0) return null;

        if ((x - b.start.x) / b.direction.x < 0) return null;
        if ((y - b.start.y) / b.direction.y < 0) return null;

        Point p = new Point(x, y);
        return p;
    }
}
