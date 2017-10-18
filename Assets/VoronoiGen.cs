using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiGen : MonoBehaviour {

    public GameObject point;
    public GameObject point2;
    public GameObject cube;

    // Use this for initialization
    void Start () {

        Voronoi v = new Voronoi();
        List<Point> vecs = new List<Point>();
        Random.InitState(1);
        List<Tile> tiles = new List<Tile>();
        int w = 100;
        for (int i = 0; i < 20; i++) {
            vecs.Add(new Point(Random.Range(0, w), Random.Range(0, w)));
            tiles.Add(new Tile(vecs[i]));
            GameObject p1 = Instantiate(point);
            p1.transform.position = new Vector3(vecs[i].x, 0, vecs[i].y);
			p1.name = "center" + vecs[i];
            
        }

        EventQueue events = new EventQueue();

        //initialze an event queue with all site events
        foreach (Point vec in vecs) {
            events.Add(new Event(vec, true));
        }

        List<Edge> edges = v.GetEdges(vecs, w, w);

        GameObject p = Instantiate(point);
        p.transform.position = new Vector3(0, 0, 0);
        p.name = "corner(0, 0)";
        p = Instantiate(point);
        p.transform.position = new Vector3(w, 0, 0);
        p.name = "corner(100, 0)";
        p = Instantiate(point);
        p.transform.position = new Vector3(0, 0, w);
        p.name = "corner(0, 100)";
        p = Instantiate(point);
        p.transform.position = new Vector3(w, 0, w);
        p.name = "corner(100, 100)";

        foreach(Tile t in tiles) {
            //collect all vertices for tile t
            foreach(Edge e in edges) { 

                if (t.center == e.left || t.center == e.right) {
                    if (e.start != null /*&& 
                        e.start.within( new Vector2(0,0) , new Vector2(w,w))*/) {
						Vector2 v1 = new Vector2(e.start.x, e.start.y);
						if (!t.verts.Contains(v1))
							t.verts.Add(v1);
					}
					if (e.end != null /*&&
                        e.end.within(new Vector2(0, 0), new Vector2(w, w))*/) {
						Vector2 v2 = new Vector2 (e.end.x, e.end.y);
						if(!t.verts.Contains(v2))
							t.verts.Add(v2);
					}
                 }
            }

            //sort by x then y
            t.verts.Sort(delegate (Vector2 v1, Vector2 v2) {
                int x = v1.x.CompareTo(v2.x);
                if (x != 0) return x;
                else return v1.y.CompareTo(v2.y);
            });

            t.normalize();
            Vector3[] meshVecs = t.asVector3Array();

//            for (int i = 0; i < meshVecs.Length; ++i) {
//                
//                float y = Mathf.PerlinNoise(Mathf.Floor(t.center.x + meshVecs[i].x)/ w * 20, Mathf.Floor(t.center.y + meshVecs[i].z)/ w * 20)*100;
//                meshVecs[i] += new Vector3(0, y, 0);
//            }

            //place spheres at verts
            foreach (Vector3 vec in meshVecs) {
                GameObject p1 = Instantiate(point2);
                p1.transform.position = t.center.toVector3() + vec;
                p1.name = "voronoi (" + p1.transform.position.x + ", " + p1.transform.position.z + ")";
            }

            //calculate traingulations
            int[] triangles = t.triangulate();

            //create meshes
            Mesh tMesh = new Mesh();

            tMesh.vertices = meshVecs;
            tMesh.triangles = triangles;
            GameObject go = Instantiate(cube);
            go.transform.position = new Vector3(t.center.x, 0, t.center.y);
            go.GetComponent<MeshFilter>().mesh = tMesh;
        }

    }

}
