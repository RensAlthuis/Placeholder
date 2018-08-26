using UnityEngine;

public class TileObject{
    private GameObject obj;
    private Vector3[] hull;

    public TileObject(GameObject parent, int index, Vector3 pos, Vector3[] hull, int height){
        this.hull = hull;

        obj = new GameObject();
        obj.name = "Tile" + index;
        obj.transform.position = pos;
        obj.transform.SetParent(parent.transform);
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        Vector3[] verts = new Vector3[hull.Length*2 + 1];
        verts[0] = new Vector3(0, 0, 0);
        for(int i = 1; i < hull.Length + 1; i++){
            verts[i] = hull[i-1];
        }

        int n = 0;
        for(int i = hull.Length + 1; i < hull.Length + 1 + hull.Length; i++){
            verts[i] = new Vector3(hull[n].x, -height, hull[n].z);
            n++;
        }

        mesh.vertices = verts;
        mesh.triangles = Triangles();
        obj.GetComponent<MeshFilter>().mesh = mesh;

    }

    public void setMaterial(Material mat){
        obj.GetComponent<MeshRenderer>().material = mat;
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