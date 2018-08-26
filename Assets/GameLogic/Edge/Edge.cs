using csDelaunay;
using UnityEngine;

public class Edge {
    public GameObject obj;

    // The equation of the edge: ax + by = c

    // TODO: Actually having an edge instead of an empty class

    public Edge (GameObject parent, EdgeDelaunay e) {
        obj = new GameObject();
        obj.name = "Edge" + e.EdgeIndex;
        obj.transform.SetParent(parent.transform);
    }
}
