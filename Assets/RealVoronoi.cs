using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

public class RealVoronoi : MonoBehaviour {

    public GameObject pointPrefabRed;
    public GameObject pointPrefabGreen;
    public int polygonNumber = 200;
    private Dictionary<Vector2f, Site> sites;
    private List<Edge> edges;

    private List<Vector2f> CreateRandomPoint()
    {
        // Use Vector2f, instead of Vector2
        // Vector2f is pretty much the same than Vector2, but like you could run Voronoi in another thread
        List<Vector2f> points = new List<Vector2f>();
        for (int i = 0; i < polygonNumber; i++)
        {
            points.Add(new Vector2f(Random.Range(0, 512), Random.Range(0, 512)));
        }

        return points;
    }

    void Start()
    {
        //creating points
        List<Vector2f> points = CreateRandomPoint();
        foreach (Vector2f v in points)
        {
            GameObject go = Instantiate(pointPrefabRed);
            go.transform.position = new Vector3(v.x, 0, v.y);
        }
        Rectf bounds = new Rectf(0, 0, 512, 512);

        //creating actual voronoi diagram
        Voronoi voronoi = new Voronoi(points, bounds, 1);

        //retrieve results
        sites = voronoi.SitesIndexedByLocation;
        edges = voronoi.Edges;

        //create Unity stuff
        foreach(Edge e in edges)
        {
            if (e.ClippedEnds == null) continue;
            GameObject go = Instantiate(pointPrefabGreen);
            Vector3 vec = new Vector3(e.ClippedEnds[LR.LEFT].x,
                                      0,
                                      e.ClippedEnds[LR.LEFT].y);
            go.transform.position = vec;
            go = Instantiate(pointPrefabGreen);
            Vector3 vec2 = new Vector3(e.ClippedEnds[LR.RIGHT].x,
                                       0,
                                       e.ClippedEnds[LR.RIGHT].y);
            go.transform.position = vec2;
            Debug.DrawLine(vec, vec2);
        }
    }

    // Update is called once per frame
    void Update () {
        foreach(Edge e in edges)
        {
            if (e.ClippedEnds == null) continue;
            Vector3 vec = new Vector3(e.ClippedEnds[LR.LEFT].x,
                                      0,
                                      e.ClippedEnds[LR.LEFT].y);
            Vector3 vec2 = new Vector3(e.ClippedEnds[LR.RIGHT].x,
                                       0,
                                       e.ClippedEnds[LR.RIGHT].y);
            Debug.DrawLine(vec, vec2);
        }
	}
}

