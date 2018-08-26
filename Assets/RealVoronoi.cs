using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

public class RealVoronoi : MonoBehaviour
{

    public Material mat;
    public int polygonNumber = 200;
    private Dictionary<Vector2f, Site> sites;
    private GameObject pointcontainer;

    private List<Vector2f> CreateRandomPoint(Rectf bounds)
    {
        List<Vector2f> points = new List<Vector2f>();
        for (int i = 0; i < polygonNumber; i++)
        {
            points.Add(new Vector2f(Random.Range(bounds.left, bounds.right), Random.Range(bounds.bottom, bounds.top)));
        }

        return points;
    }

    void Start()
    {

        //creating points
        Rectf bounds = new Rectf(0, 0, 500, 500);

        List<Vector2f> points = CreateRandomPoint(bounds);

        //creating actual voronoi diagram, with 2 lloyd relaxation thingies
        Voronoi voronoi = new Voronoi(points, bounds, 2);

        //retrieve results
        sites = voronoi.SitesIndexedByLocation;

        //create Unity stuff
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}

