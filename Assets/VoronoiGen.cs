using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiGen : MonoBehaviour {

    public GameObject point;
    public GameObject point2;

    // Use this for initialization
    void Start () {

        Voronoi v = new Voronoi();
        List<Vector2> vecs = new List<Vector2>();
        Random.InitState(1);

        for (int i = 0; i < 15; i++) {
            vecs.Add(new Vector2(Random.Range(0, 100), Random.Range(0, 100)));
            GameObject p = Instantiate(point);
            p.transform.position = new Vector3(vecs[i].x, vecs[i].y, 0);
        }



        List<Edge> edges = v.GetEdges(vecs, 100, 100);

        foreach (Edge e in edges) {
            Debug.Log("(" + e.start.x.ToString() + ", " + e.start.y.ToString() + "), (" + e.end.x.ToString() + ", " + e.end.y.ToString() + ")");
            GameObject p = Instantiate(point2);
            p.transform.position = new Vector3(e.start.x, e.start.y, 0);
            GameObject p2 = Instantiate(point2);
            p2.transform.position = new Vector3(e.end.x, e.end.y, 0);
        }

    }

}
