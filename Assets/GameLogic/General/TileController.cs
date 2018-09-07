using UnityEngine;
using MapEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour{

    [SerializeField] private GameObject tilePrefab;

    private TileData[] tiles;

    public int lengthX = 200;
    public int lengthY = 200;
    public int amountTiles = 2000;
    public int roughness = 5;
    public int heightDifference = 10;

    public void Start(){
        GameObject tileMapObj = new GameObject(){name = "TileMap"};
        TileMap tileMap = tileMapObj.AddComponent<TileMap>();
        tiles = MapGenerator.NewMap(tileMap, lengthX, lengthY, amountTiles, roughness, heightDifference, tilePrefab);
    }

    public TileData GetTile(int index){
        return tiles[index];
    }
}
