using UnityEngine;

public class Controller : MonoBehaviour{

    public Material mat;
    public Material water;

    public void Start(){
        TerrainTypes.preloadMaterials();

        MapGenerator mapGen = new MapGenerator(mat,water, 200, 200, 2000, 5, 10);
        mapGen.newMap();
    }
}