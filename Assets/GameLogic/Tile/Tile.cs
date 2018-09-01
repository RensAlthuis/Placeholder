using MapGraphics;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile {
    public Tile[] neighbors;
    private List<Edge> edges = new List<Edge>();
    MapController tileMap;

    private TileObject tileObj;
    private int height;

    private TerrainType type;
    public TerrainType Type { get { return type; } }

    public Tile (MapController tileMap, TileObject obj, TerrainType type) {
        this.tileMap = tileMap;
        tileObj = obj;
        this.type = type;
    }

    public override string ToString(){
        Vector3 pos = tileObj.transform.position;
        return pos.x + ", " + pos.y + ", " + pos.z;
    }

    public void addEdge(Edge e){
        edges.Add(e);
    }

    public void Select(){
        tileObj.setSelected(type);
        GameObject.Find("Unit").GetComponent<Unit>().moveToTile(tileObj); //this shouldn't be here // should be in tilemap
    }

    public void Deselect(){
        tileObj.setDeselected(type);
    }
}