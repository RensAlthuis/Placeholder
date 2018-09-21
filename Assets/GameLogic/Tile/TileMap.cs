using UnityEngine;
using MapEngine;
using UnityEngine.UI;

public class TileMap : MonoBehaviour{

    private static KeyCode UNSELECT = KeyCode.Escape;
    [SerializeField] private GameObject tilePrefab;

    private TileData[] tiles;

    public int lengthX = 200;
    public int lengthY = 200;
    public int amountTiles = 2000;
    public int roughness = 5;
    public int heightDifference = 10;

    public void Start(){
        tiles = MapGenerator.NewMap(this, lengthX, lengthY, amountTiles, roughness, heightDifference, tilePrefab);
    }

    public TileData GetTile(int index){
        return tiles[index];
    }
}
