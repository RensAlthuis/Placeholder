using csDelaunay;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    public GameObject obj;

    private List<Site> neighbors;
    private List<Edge> edges;

    private TileMesh tileMesh;
    private int height;

    public Tile (GameObject parent, Site s, Rectf bounds, float height) {
        //neighbors = s.NeighborSites(); // TODO: why does this not work?

        obj = new GameObject();
        obj.name = "Tile" + s.SiteIndex;
        obj.transform.position = new Vector3(s.x, 0, s.y);
        obj.transform.SetParent(parent.transform);

        // Generate the tileMesh
        tileMesh = new TileMesh(obj, height, s.Region(bounds).ConvertAll(x => new Vector3(x.x - s.x, height, x.y - s.y)).ToArray());
    }
}
