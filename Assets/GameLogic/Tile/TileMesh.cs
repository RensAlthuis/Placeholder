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
        Vector3[] normals = Normals(verts, hull.Length);
        Vector2[] uvs = Uvs(verts, hull);

        mesh.vertices = verts;
        mesh.triangles = Triangles(hull.Length);
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
        for(int i = 0; i < verts.Length; i++){
            uvs[i] = new Vector2((verts[i].x+minX)/div, (verts[i].z+minY)/div);
        }
        return uvs;
    }

    public static Vector3[] Normals(Vector3[] verts, int numCorners){
        Vector3[] normals = new Vector3[verts.Length];
        //top verts
        for(int i = 0; i < numCorners+1; i++){
            normals[i] = Vector3.up;
        }

        int offset = numCorners+1;
        for(int i = 0; i< numCorners; i++){
            int n = offset + i*4;
            Vector3 A = verts[n] - verts[n+1];
            Vector3 B = verts[n+2] - verts[n+1];
            Vector3 dir = Vector3.Cross(A, B).normalized;
            normals[n] = dir;
            normals[n + 1] = dir;
            normals[n + 2] = dir;
            normals[n + 3] = dir;
        }

        return normals;
    }

    private static Vector3[] Verts(Vector3[] hull){
        int numCorners = hull.Length;
        Vector3[] verts = new Vector3[1 + numCorners*5];
        verts[0] = Vector3.zero;

        Vector3 p;
        Vector3 p1;

        int n;
        int offset = numCorners;
        for(int i = 0; i < numCorners -1; i++){
            p = hull[i];
            p1 = hull[i+1];

            //Top plane
            verts[i+1] = p;

            n = i*4 + 1;
            //Side plane
            verts[offset + n] = p;
            verts[offset + n+1] = new Vector3(p.x, p.y - DEPTH, p.z);
            verts[offset + n+2] = new Vector3(p1.x, p1.y - DEPTH, p1.z);
            verts[offset + n+3] = p1;
        }

            //fix loopback
            n = numCorners -1;
            verts[n+1] = hull[n];
            verts[offset + n*4+1] = hull[n];
            verts[offset + n*4+2] = new Vector3(hull[n].x, hull[n].y - DEPTH, hull[n].z);
            verts[offset + n*4+3] = new Vector3(hull[0].x, hull[0].y - DEPTH, hull[0].z);
            verts[offset + n*4+4] = hull[0];
            return verts;
    }

    private static int[] Triangles(int numCorners){

        int[] triangles = new int[numCorners  * 9];
        int offset = numCorners*3;
        int n;
        for(int i = 0; i < numCorners-1; i++){
            //top plane
            triangles[i*3] = 0;
            triangles[i*3+2] = i+1;
            triangles[i*3+1] = i+2;

            // //side planes
            n = i*6;
            triangles[offset + n]   = numCorners + i*4+1;
            triangles[offset + n+2] = numCorners + i*4+2;
            triangles[offset + n+1] = numCorners + i*4+3;
            triangles[offset + n+3] = numCorners + i*4+3;
            triangles[offset + n+5] = numCorners + i*4+4;
            triangles[offset + n+4] = numCorners + i*4+1;
        }
        //fix loopback
        int a = numCorners-1;
        triangles[a*3]   = 0;
        triangles[a*3+2] = a+1;
        triangles[a*3+1] = 1;

        n = a*6;
        triangles[offset + n]   = numCorners + a*4+1;
        triangles[offset + n+2] = numCorners + a*4+2;
        triangles[offset + n+1] = numCorners + a*4+3;
        triangles[offset + n+3] = numCorners + a*4+3;
        triangles[offset + n+5] = numCorners + a*4+4;
        triangles[offset + n+4] = numCorners + a*4+1;

        return triangles;
    }

}