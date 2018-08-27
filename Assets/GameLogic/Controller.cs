using UnityEngine;

public class Controller : MonoBehaviour{

    public void Start(){
        MapGenerator mapGen = new MapGenerator(200, 200, 2000, 5, 10);
        mapGen.NewMap();
    }
}