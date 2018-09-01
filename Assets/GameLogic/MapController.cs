using UnityEngine;
using MapEngine;
using System;

public class MapController : MonoBehaviour {

    private Controller controller;
    private Tile[] tiles;

    public int lengthX = 2000;
    public int lengthY = 2000;
    public int polygonNumber = 2000;
    public int roughness = 5;
    public int heightDifference = 10;

    public void Start() {
        controller = GetComponent<Controller>();
        tiles = MapGenerator.NewMap(this, lengthX, lengthY, polygonNumber, roughness, heightDifference);
    }

    internal void SetSelected(Tile tile) {
        if(controller.selected != null) controller.selected.Select(); // feedback to the current selected object
        controller.selected = tile;
        // interaction with tile
    }

    internal void SetDeselected(Tile tile) {
        controller.selected = null;
    }

    // maybe all global graphical map stuff also here? like fog and random birds
}
