using UnityEngine;

public class Controller : MonoBehaviour{

    public void Start(){
        TerrainTypes.preloadMaterials();

        MapGenerator mapGen = new MapGenerator(200, 200, 2000, 5, 10);
        mapGen.newMap();
    }
}