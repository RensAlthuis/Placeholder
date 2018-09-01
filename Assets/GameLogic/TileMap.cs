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
        tiles = MapGenerator.NewMap(this, lengthX, lengthY, polygonNumber, roughness, heightDifference);
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