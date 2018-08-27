﻿using csDelaunay;
using MapGraphics;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    private List<Site> neighbors;
    private List<Edge> edges;

    private TileObject tileObj;
    private int height;

    private Vector2f pos;
    public Vector2f Pos { get { return pos; } }

    private MapGraphics.Terrain type;

    public Tile (GameObject parent, Site s, float height, MapGraphics.Terrain type, Rectf bounds) {
        //neighbors = s.NeighborSites(); // TODO: why does this not work?
        this.type = type;

        pos = s.Coord;
        s.tile = this;
        tileObj = new TileObject(parent, s, height, type, bounds);
        edges = new List<Edge>();
    }

    public override string ToString(){
        string str = Pos.ToString() + ", ";
        foreach(Edge e in edges) { str += e; }
        return str;
    }

    public void addEdge(Edge e){
        edges.Add(e);
    }
}
