using csDelaunay;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    private List<Site> neighbors;
    private List<Edge> edges;

    private TileObject tileMesh;
    private int height;
    private Site s;
    public Vector2 Pos{get{return new Vector2(s.x, s.y);}}

    private TerrainTypes.Type type;

    public Tile (Site s, TileObject tileMesh, TerrainTypes.Type type) {
        //neighbors = s.NeighborSites(); // TODO: why does this not work?
        this.tileMesh = tileMesh;
        this.s = s;
        this.type = type;
        tileMesh.setMaterial(TerrainTypes.GetMaterial(type));
        s.tile = this;
        edges = new List<Edge>();
    }

    public override string ToString(){
        string str = "";
        str += Pos.ToString();
        str += ", ";
        foreach(Edge e in edges){
            str += e;
        }
        return str;
    }

    public void addAdge(Edge e){
        edges.Add(e);
    }
}
