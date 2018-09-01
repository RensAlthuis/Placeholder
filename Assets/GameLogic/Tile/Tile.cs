using MapGraphics;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile {
    public Tile[] neighbors;
    private List<Edge> edges = new List<Edge>();
    TileMap tileMap;

    private TileObject tileObj;
    private int height;

    private TerrainType type;
    public TerrainType Type { get { return type; } }

    public Tile (TileMap tileMap, TileObject obj, TerrainType type) {
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
        float H, S, V;
        Color.RGBToHSV(type.GetMaterial().color, out H, out S, out V);
        Color c = Color.HSVToRGB(H, S, 1);
        tileObj.setColor(c);
        GameObject.Find("Unit").GetComponent<Unit>().moveToTile(tileObj); //this shouldn't be here
    }

    public void Deselect(){
        tileObj.setColor(type.GetMaterial().color);
    }
}