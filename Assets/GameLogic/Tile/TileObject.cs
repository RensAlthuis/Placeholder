using UnityEngine;
using MapGraphics;

public class TileObject : MonoBehaviour {

    // CONSTANTS
    private static int DEPTH = -30;
    private int numCorners;
    public int index;

    public void Init(GameObject parent, int index, Vector3 pos, Vector3[] hull, Material mat){

        // 1) Creating the game object
        this.index = index;
        name = "Tile" + index;
        numCorners = hull.Length;
        transform.position = pos;
        transform.SetParent(parent.transform);

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
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = mat;
        gameObject.AddComponent<MeshCollider>();
    }

    private void OnMouseDown() {
        TileMap map = gameObject.GetComponentInParent<TileMap>();
        map.tiles[index].OnMouseDown();
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