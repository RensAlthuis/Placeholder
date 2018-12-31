using csDelaunay;
using UnityEngine;

public class Edge {

    private Tile left;
    private Tile right;

    public Edge (GameObject parent, EdgeDelaunay e) {
        left  = e.LeftSite.tile;
        right = e.RightSite.tile;
        left.addEdge(this);
        right.addEdge(this);
    }
}
