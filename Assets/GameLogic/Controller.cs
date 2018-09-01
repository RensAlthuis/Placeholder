using UnityEngine;

public interface Selectable { // All things that could be selected // too small for its own file
    void Select(); // using this on something that is already selected should deselect it
}

public class Controller : MonoBehaviour {

    public Selectable selected;

    void Start () {

    }
}