using UnityEngine;

public interface Selectable { // All things that could be selected // too small for its own file
    void Select();
}

public class Controller : MonoBehaviour {

    public Selectable selected;

    void Start () {

    }
}