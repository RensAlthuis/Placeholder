using csDelaunay;
using MapGraphics;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile {
    private List<Site> neighbors;
    private List<Edge> edges = new List<Edge>();

    private TileObject tileObj;
    private int height;

    private Vector2f pos;
    public Vector2f Pos { get { return pos; } }

    private TerrainType type;
    public TerrainType Type { get { return type; } }

    public Tile (GameObject parent, Site s, float height, TerrainType type, Rectf bounds) {
        //neighbors = s.NeighborSites(); // TODO: why does this not work?
        this.type = type;

        pos = s.Coord;
        s.tile = this;
        (UnityEngine.Object.Instantiate(Resources.Load("TileObject")) as GameObject).AddComponent<TileObject>().Create(this,parent,s,height,type,bounds);
    }

    public override string ToString(){
        string str = Pos.ToString() + ", ";
        foreach(Edge e in edges) { str += e; }
        return str;
    }

    public void addEdge(Edge e){
        edges.Add(e);
    }


    internal void OnMouseDown() {
        // do stuff
    }
}