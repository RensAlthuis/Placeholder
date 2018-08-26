using UnityEngine;

class Controller : MonoBehaviour{

    public Material mat;

    public void Start(){
        MapGenerator mapGen = new MapGenerator(mat, 2000, 2000, 2000, 300, 100);
        mapGen.newMap();
    }
}