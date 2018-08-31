using UnityEngine;

public class Controller : MonoBehaviour{

    public int lengthX = 2000;
    public int lengthY = 2000;

    public Unit unit;

    public void Start(){
        Instantiate(unit).name = "Unit";

    }
}