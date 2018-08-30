using UnityEngine;
using System;

public class TileMap : MonoBehaviour {


    public Tile[] tiles;

    public Tile selected;

    public int lengthX = 2000;
    public int lengthY = 2000;
    public int polygonNumber = 2000;
    public int roughness = 5;
    public int heightDifference = 10;

    private Rectf bounds;

    public void Start(){
        MapGenerator.setOptions(lengthX, lengthY, polygonNumber, roughness, heightDifference);
        // GameObject tiles = new GameObject() { name = "Tiles" };
        tiles = MapGenerator.NewMap(this);
    }

    public void setSelected(Tile tile){
        if(selected != tile){
            tile.Select();
            if(selected != null)
                selected.Deselect();
            selected = tile;
        }
    }
}