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
        // Vector3[] verts = new Vector3[numCorners *2 + 1];
        // verts[0] = new Vector3(0, 0, 0);

        // for(int i = 1; i < numCorners  + 1; i++) {
        //     verts[i] = hull[i-1];
        // }

        // for(int i = numCorners  + 1, n = 0; i < numCorners  + 1 + numCorners ; i++, n++) {
        //     verts[i] = new Vector3(hull[n].x, DEPTH, hull[n].z);
        // }

        // Vector3[] normals = new Vector3[verts.Length];
        // for(int i = 0; i < verts.Length; i++){
        //     normals[i] = Vector3.up;
        // }

        Vector3[] verts = Verts(hull);

        int[] triangles = new int[verts.Length];
        for(int i = 0; i < verts.Length; i++){
            triangles[i] = i;
        }
        float minX = Mathf.Infinity;
        float minY = Mathf.Infinity;
        for(int i = 0; i < hull.Length; i++){
            if(minX > hull[i].x) minX = hull[i].x;
            if(minY > hull[i].z) minY = hull[i].z;
        }
        minX = -minX;
        minY = -minY;
        float div = Mathf.NegativeInfinity;
        for(int i = 0; i < hull.Length; i++){
            if(div < hull[i].x+minX) div = hull[i].x + minX;
            if(div < hull[i].z+minY) div = hull[i].z + minY;
        }
        Vector2[] uvs = Uvs(verts, minX, minY, div);

        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        // mesh.normals = normals;
        // mesh.triangles = Triangles();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        return this;
    }

    private void OnMouseDown() {
        Debug.Log("Click" + index);
        tileMap.setSelected(tileMap.tiles[index]);
    }

    public void setColor(Color col){
        GetComponent<MeshRenderer>().material.color = col;
    }


    //TODO: fix side panels!
    private Vector2[] Uvs(Vector3[] verts, float minX, float minY, float div){
        Vector2[] uvs = new Vector2[verts.Length];
        Vector2 mid = new Vector2(0.5f, 0.5f);
        Debug.Log(index);
        for(int i = 0; i < verts.Length; i++){
            // uvs[i*3] = mid;
            uvs[i] = new Vector2((verts[i].x+minX)/div, (verts[i].z+minY)/div);
        }
        return uvs;
    }

    private Vector3[] Verts(Vector3[] hull){
        Vector3[] verts= new Vector3[numCorners  * 9];

        int offset = numCorners  * 3;

        Vector3 p;
        Vector3 p1;
        for(int i = 0; i < numCorners-1 ; i++){
            p = hull[i];
            p1 = hull[i+1];

            //top plane
            verts[i*3] = Vector3.zero;
            verts[i*3+2] = p;
            verts[i*3+1] = p1;

            //side planes
            verts[offset + (i*6)] = p;
            verts[offset + (i*6)+2] = new Vector3(p.x, p.y - 30, p.z);
            verts[offset + (i*6)+1] = new Vector3(p1.x, p1.y -30, p1.z);
            verts[offset + (i*6)+3] = p;
            verts[offset + (i*6)+5] = new Vector3(p1.x, p1.y -30, p1.z);
            verts[offset + (i*6)+4] = p1;
        }
        //fix loopback
        int index = numCorners -1;

        verts[index*3] = Vector3.zero;
        verts[index*3+2] = hull[index];
        verts[index*3+1] = hull[0];

        p = hull[index];
        p1 = hull[0];
        verts[offset + (index*6)] = p;
        verts[offset + (index*6)+2] = new Vector3(p.x, p.y - 30, p.z);
        verts[offset + (index*6)+1] = new Vector3(p1.x, p1.y -30, p1.z);
        verts[offset + (index*6)+3] = p;
        verts[offset + (index*6)+5] = new Vector3(p1.x, p1.y -30, p1.z);
        verts[offset + (index*6)+4] = p1;

        return verts;
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