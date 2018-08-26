using csDelaunay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile{
    public GameObject obj;

    private TileMesh tileMesh;
    private int height;

    public Tile (GameObject parent, Site s, Rectf bounds, float height) {
        obj = new GameObject();
        obj.name = "Tile" + s.SiteIndex;
        obj.transform.position = new Vector3(s.x, 0, s.y);
        obj.transform.SetParent(parent.transform);

        // Generate the tileMesh
        tileMesh = new TileMesh(obj, height, s.Region(bounds).ConvertAll(x => new Vector3(x.x - s.x, height, x.y - s.y)).ToArray());
    }
}
