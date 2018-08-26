using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

public class RealVoronoi : MonoBehaviour
{

    public GameObject pointPrefabRed;
    public GameObject pointPrefabGreen;
    public Material mat;
    public int polygonNumber = 200;
    private Dictionary<Vector2f, Site> sites;
    private List<Edge> edges;
    private GameObject pointcontainer;

    private List<Vector2f> CreateRandomPoint(Rectf bounds)
    {
        // Use Vector2f, instead of Vector2
        // Vector2f is pretty much the same than Vector2, but like you could run Voronoi in another thread
        List<Vector2f> points = new List<Vector2f>();
        for (int i = 0; i < polygonNumber; i++)
        {
            points.Add(new Vector2f(Random.Range(bounds.left, bounds.right), Random.Range(bounds.bottom, bounds.top)));
        }

        return points;
    }

    void Start()
    {
        pointcontainer = new GameObject();
        pointcontainer.name = "pointcontainer";
        //creating points
        Rectf bounds = new Rectf(0, 0, 500, 500);

        List<Vector2f> points = CreateRandomPoint(bounds);

        //creating actual voronoi diagram
        Voronoi voronoi = new Voronoi(points, bounds, 2);

        //retrieve results
        sites = voronoi.SitesIndexedByLocation;
        edges = voronoi.Edges;


        //create Unity stuff

        foreach (Site site in sites.Values)
        {
            GameObject go = Instantiate(pointPrefabRed);
            go.transform.position = new Vector3(site.x, 0, site.y);
            go.transform.SetParent(pointcontainer.transform);
        }

        List<TileMesh> meshes = new List<TileMesh>();
        GameObject meshescontainer = new GameObject();
        meshescontainer.name = "meshescontainer";
        foreach (Site s in sites.Values){
            int y = Random.Range(1, 10);
            TileMesh mesh = new TileMesh(meshescontainer, new Vector3(0, y, 0), s.Region(bounds).ConvertAll(x=>new Vector3(x.x-s.x, y, x.y-s.y)).ToArray());
            meshes.Add(mesh);
            mesh.obj.transform.position = new Vector3(s.x, 0, s.y);
            mesh.obj.GetComponent<MeshRenderer>().material = mat;
        }

        foreach (Edge e in edges)
        {
            if (e.ClippedEnds == null) continue;
            GameObject go = Instantiate(pointPrefabGreen);
            Vector3 vec = new Vector3(e.ClippedEnds[LR.LEFT].x,
                                      0,
                                      e.ClippedEnds[LR.LEFT].y);
            go.transform.position = vec;
            go.transform.SetParent(pointcontainer.transform);
            go = Instantiate(pointPrefabGreen);
            Vector3 vec2 = new Vector3(e.ClippedEnds[LR.RIGHT].x,
                                       0,
                                       e.ClippedEnds[LR.RIGHT].y);
            go.transform.position = vec2;
            go.transform.SetParent(pointcontainer.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // foreach (Site s in sites.Values)
        // {
        //     foreach (Edge e in s.Edges)
        //     {
        //         if(!e.Visible())continue;
        //         Debug.DrawLine(new Vector3(e.ClippedEnds[LR.LEFT].x, 0, e.ClippedEnds[LR.LEFT].y),
        //                        new Vector3(e.ClippedEnds[LR.RIGHT].x, 0, e.ClippedEnds[LR.RIGHT].y), Color.red);
        //     }

        //     //foreach(Site s2 in s.NeighborSites()){
        //     //    Debug.DrawLine(new Vector3(s.x, 0, s.y), new Vector3(s2.x, 0, s2.y), Color.green);
        //     //}
        // }
    }
}

