using UnityEngine;

public class Controller : MonoBehaviour{

    public int lengthX = 2000;
    public int lengthY = 2000;
    public int polygonNumber = 2000;
    public int roughness = 5;
    public int heightDifference = 10;

    public void Start(){
        MapGenerator mapGen = new MapGenerator(lengthX, lengthY, polygonNumber, roughness, heightDifference);
        mapGen.NewMap();
    }
}