using csDelaunay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public GameObject obj;

    private TileMesh tileMesh;

    public Tile (GameObject parent, Site s, Rectf bounds) {
        obj = new GameObject();
        obj.name = "Tile" + s.SiteIndex;
        obj.transform.position = new Vector3(s.x, 0, s.y);
        obj.transform.SetParent(parent.transform);

        // Generate the tileMesh
        int height = Random.Range(1, 10); // TODO: dependend on tile
        tileMesh = new TileMesh(obj, height, s.Region(bounds).ConvertAll(x => new Vector3(x.x - s.x, height, x.y - s.y)).ToArray());
    }
}
