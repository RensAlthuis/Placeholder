using UnityEngine;

public class Controller : MonoBehaviour{

    public int nTiles;
    public void Start(){
        // MapGenerator mapGen = new MapGenerator(1000, 500, 20000, 5, 10);
        MapGenerator mapGen = new MapGenerator(1000, 500, nTiles, 5, 10);
        mapGen.NewMap();
    }
}