using UnityEngine;

public class Controller : MonoBehaviour{

    public Material mat;
    public Material water;

    public void Start(){
        TerrainTypes.preloadMaterials();

        MapGenerator mapGen = new MapGenerator(mat,water, 2000, 2000, 2000, 300, 100);
        mapGen.newMap();
    }
}