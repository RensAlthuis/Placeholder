using csDelaunay;
using UnityEngine;

public class EdgeObject {

    private GameObject obj;

    public EdgeObject (GameObject parent, EdgeDelaunay e) {
        obj = new GameObject();
        obj.name = "Edge" + e.EdgeIndex;
        obj.transform.SetParent(parent.transform);
    }
}
