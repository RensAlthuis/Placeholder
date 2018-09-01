using UnityEngine;

public interface Selectable { // All things that could be selected // too small for its own file
    bool Select();
}

public class Controller : MonoBehaviour{

    public Selectable selected;

    public void Start(){
        //Instantiate(unit).name = "Unit";

    }
}