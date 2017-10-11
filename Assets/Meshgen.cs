using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meshgen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Mesh m = new Mesh();

        Vector3[] verts = new Vector3[4];

        verts[0] = new Vector3(1, 1, 0);
        verts[1] = new Vector3(1, -1, 0);
        verts[2] = new Vector3(-1, -1, 0);
        verts[3] = new Vector3(-1, 1, 0);
        m.vertices = verts;

        m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        GetComponent<MeshFilter>().mesh = m;

    }

}
