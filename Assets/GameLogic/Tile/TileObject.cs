using csDelaunay;
using UnityEngine;
using MapGraphics;

public class TileObject : MonoBehaviour {
    
    // CONSTANTS
    private static int DEPTH = -30;

    private Vector3[] hull;
    private Tile tile;

    public static void Create(Tile tile, GameObject parent, Site s, float height, TerrainType type, Rectf bounds) { // yey nicer code
        GameObject obj = (Instantiate(new GameObject()) as GameObject);
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshCollider>();
        obj.AddComponent<MeshRenderer>();
        obj.AddComponent<TileObject>().Set(tile, parent, s, height, type, bounds);
    }

    private void Set(Tile tile, GameObject parent, Site s, float height, TerrainType type, Rectf bounds) {
        hull = s.Region(bounds).ConvertAll(x => new Vector3(x.x - s.x, 0, x.y - s.y)).ToArray();
        this.tile = tile;

        // 1) Creating the game object
        name = "Tile" + s.SiteIndex;
        transform.position = new Vector3(s.x, height, s.y);
        transform.SetParent(parent.transform);

        // 2) Creating the mesh
        Mesh mesh = new Mesh();
        Vector3[] verts = new Vector3[hull.Length*2 + 1];
        verts[0] = new Vector3(0, 0, 0);
        for(int i = 1; i < hull.Length + 1; i++) { verts[i] = hull[i-1]; }
        for(int i = hull.Length + 1, n = 0; i < hull.Length + 1 + hull.Length; i++, n++) {
            verts[i] = new Vector3(hull[n].x, DEPTH, hull[n].z);
        }

        mesh.vertices = verts;
        mesh.triangles = Triangles();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material = type.GetMaterial();
    }

    private void OnMouseDown() {
        Debug.Log(name);
        GetComponent<MeshRenderer>().enabled = false; // as a test
        
        //tile.OnMouseDown();
    }

    private int[] Triangles(){
        int[] triangles = new int[hull.Length * 9];

        int offset = hull.Length * 3;
        for(int i = 0; i < hull.Length; i++){
            //top plane
            triangles[i*3] = 0;
            triangles[i*3+2] = i+1;
            triangles[i*3+1] = i+2;

            //side planes
            triangles[offset + (i*6)] = i+1;
            triangles[offset + (i*6)+2] = i+hull.Length+1;
            triangles[offset + (i*6)+1] = i+2;
            triangles[offset + (i*6)+3] = i+2;
            triangles[offset + (i*6)+5] = i+hull.Length+1;
            triangles[offset + (i*6)+4] = i+2+hull.Length;
        }
        triangles[offset - 2] = 1;
        triangles[triangles.Length - 3] = 1;
        triangles[triangles.Length - 5] = 1;
        triangles[triangles.Length - 2] = hull.Length+1;

        return triangles;
    }
}