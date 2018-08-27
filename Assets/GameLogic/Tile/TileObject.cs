using csDelaunay;
using UnityEngine;
using MapGraphics;

public class TileObject {
    
    // CONSTANTS
    private static int DEPTH = -10;

    private GameObject obj;
    private Vector3[] hull;

    public TileObject(GameObject parent, Site s, float height, TerrainType type, Rectf bounds) {
        hull = s.Region(bounds).ConvertAll(x => new Vector3(x.x - s.x, 0, x.y - s.y)).ToArray();

        // 1) Creating the game object
        obj = new GameObject();
        obj.SetActive(false);
        obj.name = "Tile" + s.SiteIndex;
        obj.transform.position = new Vector3(s.x, height, s.y);
        obj.transform.SetParent(parent.transform);

        // 2) Creating the mesh
        Mesh mesh = new Mesh();
        Vector3[] verts = new Vector3[hull.Length*2 + 1];
        verts[0] = new Vector3(0, 0, 0);
        for(int i = 1; i < hull.Length + 1; i++){
            verts[i] = hull[i-1];
        }

        int n = 0;
        for(int i = hull.Length + 1; i < hull.Length + 1 + hull.Length; i++){
            verts[i] = new Vector3(hull[n].x, DEPTH, hull[n].z);
            n++;
        }

        Vector3[] normals = new Vector3[verts.Length];
        for(int i = 0; i < verts.Length; i++){
            normals[i] = Vector3.up;
        }

        mesh.vertices = verts;
        mesh.normals = normals;
        mesh.triangles = Triangles();
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        obj.GetComponent<MeshFilter>().mesh = mesh;
        obj.GetComponent<MeshRenderer>().material = type.GetMaterial();
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