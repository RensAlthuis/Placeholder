using csDelaunay;
using UnityEngine;

public class Edge {

    // TODO: Actually having an edge instead of an empty class

    public Tile left;
    public Tile right;

    public Edge (Tile left, Tile right) {
        this.left = left;
        this.right = right;
    }

    public override string ToString(){
        return ("(" + left.Pos + ", " + right.Pos + ")");
    }

}
