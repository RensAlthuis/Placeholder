using UnityEngine;
using MapEngine;

public class MapController : MonoBehaviour {

    private Tile[] tiles;
    private Tile selected;

    public int lengthX = 2000;
    public int lengthY = 2000;
    public int polygonNumber = 2000;
    public int roughness = 5;
    public int heightDifference = 10;

    public void Start() {
        tiles = MapGenerator.NewMap(this, lengthX, lengthY, polygonNumber, roughness, heightDifference);
    }

    public void setSelected(Tile tile) {
        selected = tile;
    }
}
