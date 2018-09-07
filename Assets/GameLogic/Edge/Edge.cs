using csDelaunay;
using UnityEngine;

public class Edge {

    private EdgeObject edgeObj;

    private GameObject left;
    private GameObject right;

    public Edge (GameObject parent, EdgeDelaunay e) {
        left  = e.LeftSite.tile;
        right = e.RightSite.tile;
        left.GetComponent<TileData>().addEdge(this);
        right.GetComponent<TileData>().addEdge(this);

        //edgeObj = new EdgeObject(parent, e);
    }

    // public override string ToString(){
    //     return ("(" + left.Pos + ", " + right.Pos + ")");
    // }
}
