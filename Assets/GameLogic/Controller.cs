using UnityEngine;

class Controller : MonoBehaviour{

    public Material mat;

    public void Start(){
        MapGenerator mapGen = new MapGenerator(mat, 500, 500, 200);
        mapGen.newMap();
    }
}