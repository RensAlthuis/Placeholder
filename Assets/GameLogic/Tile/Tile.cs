using MapEngine;
using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

public class Tile : Selectable {
    private MapController tileMap;
    private TileObject tileObj;
    
    private int height;
    private Tile[] neighbors;
    private List<Edge> edges = new List<Edge>();
    private TerrainType type;

    public Tile[] Neighbors { get { return neighbors; } }
    public List<Edge> Edges { get { return edges; } }
    public TerrainType Type { get { return type; } }

    public Tile(MapController tileMap, Site s, float height, TerrainType type, Rectf bounds, Transform tilesTransform) {
        s.tile = this;
        tileObj = TileObject.Create(this, s, height, type, bounds, tilesTransform);
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

    private bool selected;
    public bool Selected { get { return selected; } }

    public void Select() {
        if (!selected) {
            selected = true;
            tileMap.SetSelected(this);
            tileObj.SetSelected();
        } else {
            selected = false;
            tileMap.SetDeselected(this);
            tileObj.SetDeselected();
        }

        //GameObject.Find("Unit").GetComponent<Unit>().moveToTile(tileObj); //this shouldn't be here // should be in controller
    }
}