using MapEngine;
using System.Collections.Generic;
using UnityEngine;
using System;
using csDelaunay;

public class Tile : Selectable {
    private MapController tileMap;
    private TileObject tileObj;
    
    private int height;
    private Tile[] neighbors;
    private List<Edge> edges = new List<Edge>();
    private TerrainType type;
    private bool selected;

    public Tile[] Neighbors { get { return neighbors; } }
    public List<Edge> Edges { get { return edges; } }
    public TerrainType Type { get { return type; } }
    public bool Selected { get { return selected; } }

    public Tile(MapController tileMap, Site s, float height, TerrainType type, Rectf bounds) {
        tileObj = TileObject.Create(this, s, height, type, bounds, tileMap.transform);
        neighbors = s.getNeighbourTiles(); // :(
        this.tileMap = tileMap;
        this.type = type;
    }

    public override string ToString(){
        Vector3 pos = tileObj.transform.position;
        return pos.x + ", " + pos.y + ", " + pos.z;
    }

    public void addEdge(Edge e){
        edges.Add(e);
    }

    // ========================= INTERACTION ==============================

    public bool Select() {
        if (selected != tile) {
            if (selected != null) selected.Deselect(); // deselect
            tile.Select(); // select
            selected = tile;
        }
        tileObj.SetDeselected(type);

        tileObj.SetSelected(type);
        //GameObject.Find("Unit").GetComponent<Unit>().moveToTile(tileObj); //this shouldn't be here // should be in tilemap

        return true;
    }
}