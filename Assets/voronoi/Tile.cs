using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    public Point center;
    public List<Vector2> verts;

    public Tile(Point p) {
        center = p;
        verts = new List<Vector2>();
    }

    public void normalize() {
        for(int i = 0; i < verts.Count; ++i) {
            verts[i] -= center.toVector2();
        }
    }

    public Vector3[] asVector3Array() {
        List<Vector3> l = new List<Vector3>();
        foreach (Vector2 v in verts) {
            l.Add(new Vector3(v.x, 0, v.y));
        }
        return l.ToArray();
    }

    public int[] triangulate() {

        List<int> triangles = new List<int>();
        Vector2 a = verts[0];

        float[] dvalues = new float[verts.Count - 1];
        for(int index = 1; index < verts.Count; ++index) {
            dvalues[index-1] = (verts[index].y - a.y) / (verts[index].x - a.x);
        }

        for (int i = 0; i < dvalues.Length-1; ++i) {
            triangles.Add(0);
            int ld = 0;
            for (int index = 1; index < dvalues.Length; ++index) {
                if (dvalues[index] < dvalues[ld]) {
                    ld = index;
                }
            }
            dvalues[ld] = Mathf.Infinity;
            triangles.Add(ld + 1);
            ld = 0;
            for (int index = 1; index < dvalues.Length; ++index) {
                if (dvalues[index] < dvalues[ld]) {
                    ld = index;
                }
            }
            triangles.Add(ld + 1);

        }
        triangles.Reverse();
        return triangles.ToArray();
        
    }
}
