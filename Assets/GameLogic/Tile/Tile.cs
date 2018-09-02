using MapEngine;
using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

public class Tile : Selectable {
    private MainController tileMap;
    private TileObject tileObj;
    
    private int height;
    private Tile[] neighbors;
    private List<Edge> edges = new List<Edge>();
    private TerrainType type;
    private Vector3 coord;

    public Tile[] Neighbors { get { return neighbors; } }
    public List<Edge> Edges { get { return edges; } }
    public TerrainType Type { get { return type; } }
    public Vector3 Coord { get { return coord; } } // the position at which objects gat placed

    public Tile(MainController tileMap, Site s, float height, TerrainType type, Rectf bounds, Transform tilesTransform) {
        s.tile = this;
        coord = new Vector3(s.x, height, s.y);
        tileObj = TileObject.Create(this, s, height, type, bounds, tilesTransform);
        neighbors = s.getNeighbourTiles(); // :(
        this.tileMap = tileMap;
        this.type = type;
    }

    public void addEdge(Edge e){
        edges.Add(e);
    }

    // ========================= SELECTABLE ==============================

    private bool selected;
    public bool isSelected { get { return selected; } } // unnecessary

    public void Select() {
        if(tileMap.SetSelected(this)) {
            selected = true;
            tileObj.SetSelected();
        }
    }

    public bool Deselect(Selectable obj) {
        selected = false;
        tileObj.SetDeselected();
        return true;
    }

    public void Highlight() {
        tileObj.Hightlight();
    }

    public string Name() {
        return type.GetName() + " tile";
    }
}