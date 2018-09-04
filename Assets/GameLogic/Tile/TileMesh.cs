using UnityEngine;
using MapEngine;
using csDelaunay;

public static class TileMesh{

    // CONSTANTS
    private const int DEPTH = 10;

    public static Mesh Create(Vector3[] hull) {

        // Setting the object's data
        Mesh mesh = new Mesh();

        Vector3[] verts = Verts(hull);
        Vector3[] normals = Normals(verts);
        Vector2[] uvs = Uvs(verts, hull);

        mesh.vertices = verts;
        mesh.triangles = Triangles(verts.Length);
        mesh.uv = uvs;
        mesh.normals = normals;

        return mesh;

    }

    private static Vector2[] Uvs(Vector3[] verts, Vector3[] hull) {
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

        Vector2[] uvs = new Vector2[verts.Length];
        Vector2 mid = new Vector2(0.5f, 0.5f);
        for(int i = 0; i < verts.Length; i++){
            uvs[i] = new Vector2((verts[i].x+minX)/div, (verts[i].z+minY)/div);
        }
        return uvs;
    }

    public static Vector3[] Normals(Vector3[] verts){
        Vector3[] normals = new Vector3[verts.Length];
        for(int i = 0; i < verts.Length; i++){
            normals[i] = Vector3.up;
        }
        return normals;
    }

    private static Vector3[] Verts(Vector3[] hull){
        int numCorners = hull.Length;
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
            verts[offset + (i*6)+2] = new Vector3(p.x, p.y - DEPTH, p.z);
            verts[offset + (i*6)+1] = new Vector3(p1.x, p1.y - DEPTH, p1.z);
            verts[offset + (i*6)+3] = p;
            verts[offset + (i*6)+5] = new Vector3(p1.x, p1.y - DEPTH, p1.z);
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
        verts[offset + (index*6)+2] = new Vector3(p.x, p.y - DEPTH, p.z);
        verts[offset + (index*6)+1] = new Vector3(p1.x, p1.y - DEPTH, p1.z);
        verts[offset + (index*6)+3] = p;
        verts[offset + (index*6)+5] = new Vector3(p1.x, p1.y - DEPTH, p1.z);
        verts[offset + (index*6)+4] = p1;

        return verts;
    }

    private static int[] Triangles(int count){

        int[] triangles = new int[count];
        for(int i = 0; i < count; i++){
            triangles[i] = i;
        }

        return triangles;

        /* USING DUPLICATE VERTS FOR NOW, PROLLY SHOULD FIX THAT
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
        */
    }
}