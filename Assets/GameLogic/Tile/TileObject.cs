using UnityEngine;
using MapGraphics;

public class TileObject : MonoBehaviour {

    // CONSTANTS
    private static int DEPTH = -30;
    private int numCorners;
    private int index;
    private TileMap tileMap;

    public static TileObject Create(TileMap tileMap, csDelaunay.Site s, float height, Material mat, Rectf bounds) { // yey nicer code

        //Maybe move this to MapGenerator
        Vector3[] hull = s.Region(bounds).ConvertAll(x => new Vector3(x.x - s.x, 0, x.y - s.y)).ToArray();
        Vector3 pos = new Vector3(s.x, height, s.y);

        //creating the object
        GameObject obj = new GameObject();
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshCollider>();
        obj.AddComponent<MeshRenderer>();
        return obj.AddComponent<TileObject>().Init(tileMap, s.SiteIndex, pos, hull, mat);
    }

    private TileObject Init(TileMap tileMap, int index, Vector3 pos, Vector3[] hull, Material mat){

        this.tileMap = tileMap;
        this.index = index;

        // 1) setting the object's data
        name = "Tile" + index;
        numCorners = hull.Length;
        transform.position = pos;
        transform.SetParent(tileMap.transform);

        // 2) Creating the mesh
        Mesh mesh = new Mesh();
        Vector3[] verts = new Vector3[numCorners *2 + 1];
        verts[0] = new Vector3(0, 0, 0);

        for(int i = 1; i < numCorners  + 1; i++) {
            verts[i] = hull[i-1];
        }

        for(int i = numCorners  + 1, n = 0; i < numCorners  + 1 + numCorners ; i++, n++) {
            verts[i] = new Vector3(hull[n].x, DEPTH, hull[n].z);
        }

        mesh.vertices = verts;
        mesh.triangles = Triangles();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        return this;
    }

    private void OnMouseDown() {
        Debug.Log("Click" + index);
        tileMap.tiles[index].OnMouseDown();
    }

    public void setColor(Color col){
        GetComponent<MeshRenderer>().material.color = col;
    }

    private int[] Triangles(){
        int[] triangles = new int[numCorners  * 9];

        int offset = numCorners  * 3;
        for(int i = 0; i < numCorners ; i++){
            //top plane
            triangles[i*3] = 0;
            triangles[i*3+2] = i+1;
            triangles[i*3+1] = i+2;

            //side planes
            triangles[offset + (i*6)] = i+1;
            triangles[offset + (i*6)+2] = i+numCorners +1;
            triangles[offset + (i*6)+1] = i+2;
            triangles[offset + (i*6)+3] = i+2;
            triangles[offset + (i*6)+5] = i+numCorners +1;
            triangles[offset + (i*6)+4] = i+2+numCorners ;
        }
        //fix loopback
        triangles[offset - 2] = 1;
        triangles[triangles.Length - 3] = 1;
        triangles[triangles.Length - 5] = 1;
        triangles[triangles.Length - 2] = numCorners +1;

        return triangles;
    }
}