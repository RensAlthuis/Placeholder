using UnityEngine;

public class Controller : MonoBehaviour{

    public int lenghtX = 2000;
    public int lenghtY = 2000;
    public int polygonNumber = 2000;
    public int roughness = 5;
    public int heightDifference = 10;

    public void Start(){
        MapGenerator mapGen = new MapGenerator(lenghtX, lenghtY, polygonNumber, roughness, heightDifference);
        mapGen.NewMap();
    }
}