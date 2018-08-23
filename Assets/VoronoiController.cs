using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiController : MonoBehaviour {

    public GameObject pointPrefab;
    public GameObject pointPrefabGreen;
    public GameObject lineObject;
    // Use this for initialization

    private List<GameObject> pointObjList = new List<GameObject>();
    private List<GameObject> voronoiPointObjList = new List<GameObject>();
    private GameObject line;
    private List<Point> v = new List<Point>();
    private List<Point> vdir = new List<Point>();
    private bool doUpdate = false;
    private Vector4 bound = new Vector4(0, 0, 100, 100);

    private Generator g;
    private int old;

    void Start() {
        Edge e1 = new Edge("e1", new Point(0,0), new Point(0,2), new Point(2,0));
        Edge e2 = new Edge("e2", new Point(2,0), new Point(0,0), new Point(4,0));
        Debug.Log(e1.Intersect(e2));

        Debug.Log(e1.Solve(2));
        //S1();
    }
    void Update() {
        //U1();
    }

    private void MovePoints(float speed) {
        int index = 0;
        foreach (Point p in v) {
            p.x += vdir[index].x * speed;
            p.y += vdir[index].y * speed;
            if (p.x > bound.w) {
                p.x = bound.w;
                vdir[index].x *= -1;
            }
            if (p.x < bound.x) {
                p.x = bound.x;
                vdir[index].x *= -1;
            }
            if (p.y > bound.z) {
                p.y = bound.z;
                vdir[index].y *= -1;
            }
            if (p.y < bound.y) {
                p.y = bound.y;
                vdir[index].y *= -1;
            }
            pointObjList[index].transform.position = p;
            index++;
        }
        index = 0;
        foreach(Point p in v) {
            p.x = vdir[index].x * speed > 0 ? Mathf.Ceil(p.x) : Mathf.Floor(p.x);
            p.y = vdir[index].y * speed > 0 ? Mathf.Ceil(p.y) : Mathf.Floor(p.y);
            pointObjList[index].transform.position = p;
            index++;
        }
        g.Calculate();
        g.ConnectNeighbours();
        //g.FinishEdges(g.root);

    }

    private void MakePoints() {
        foreach (GameObject g in voronoiPointObjList) {
            Destroy(g);
        }

        foreach (Edge e in g.edges) {
            if (e.start != null) {
                GameObject go = Instantiate(pointPrefabGreen);
                go.transform.position = e.start;
                voronoiPointObjList.Add(go);
            }
            if (e.end != null) {
                GameObject go = Instantiate(pointPrefabGreen);
                go.transform.position = e.end;
                voronoiPointObjList.Add(go);
            }
        }
    }

    void S1() {

        v.Add(new Point(26, 98));
        v.Add(new Point(63, 62));
        v.Add(new Point(66, 45));
        v.Add(new Point(55, 45));

        foreach (Point p in v) {
            GameObject go = Instantiate(pointPrefab);
            pointObjList.Add(go);
            go.transform.position = p;
        }
        g = new Generator(v, bound);
        old = g.points.Count;
        line = Instantiate(lineObject);
    }

    // Update is called once per frame
    void U1() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            float sl = (float)g.Step();
            line.transform.position = new Vector3(50, 0, sl);
            if (g.points.Count != old) {
                GameObject go = Instantiate(pointPrefabGreen);
                Point p = g.points[g.points.Count - 1];
                go.transform.position = p;
                voronoiPointObjList.Add(go);
                old = g.points.Count;
            }
            Debug.Log(g.root);
        }

        foreach (Edge e in g.edges) {
            if (e.start != null && e.end != null)
                Debug.DrawLine(e.start, e.end);
        }

        if (Input.GetKeyDown(KeyCode.Keypad0)) {
            //g.FinishEdges(g.root);
            MakePoints();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            g.Reset(v, bound);
            MakePoints();
            old = g.points.Count;
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            g.ConnectNeighbours();
            MakePoints();
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            g.Calculate();
            MakePoints();
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            doUpdate = !doUpdate;
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            v.Add(new Point(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100)));
            GameObject go = Instantiate(pointPrefab);
            pointObjList.Add(go);
            go.transform.position = v[v.Count - 1];
        }

        if (doUpdate) {
            v.Clear();
            UnityEngine.Object[] ol = FindObjectsOfType(pointPrefab.GetType());
            foreach (GameObject o in ol) {
                if (o.tag == "Point")
                    v.Add(((Point)o.transform.position));

            }
            g.Reset(v, bound);
            g.Calculate();
            g.ConnectNeighbours();
            //g.FinishEdges(g.root);
            MakePoints();
        }
    }

    void S2() {
        int n = 5;
        for (int i = 0; i < n; i++) {
            v.Add(new Point(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100)));
        }

        for (int i = 0; i < n; i++) {
            vdir.Add(new Point(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 2f)));
        }

        foreach (Point p in v) {
            GameObject go = Instantiate(pointPrefab);
            pointObjList.Add(go);
            go.transform.position = p;
        }
        g = new Generator(v, bound);
        old = g.points.Count;
        g.Calculate();
        line = Instantiate(lineObject);
    }

    void U2() {

        if (Input.GetKey(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) {
            MovePoints(-0.1f);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.R)) {
            MovePoints(0.1f);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
            MovePoints(-1f);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.F)) {
            MovePoints(1f);
        }

        if (Input.GetKey(KeyCode.Z) || Input.GetKeyDown(KeyCode.C)) {
            MovePoints(-2f);
        }
        if (Input.GetKey(KeyCode.X) || Input.GetKeyDown(KeyCode.V)) {
            MovePoints(2f);
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            g.Reset(v, bound);
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            float sl = (float)g.Step();
            line.transform.position = new Vector3(50, 0, sl);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            v.Add(new Point(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100)));
            vdir.Add(new Point(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
            GameObject go = Instantiate(pointPrefab);
            pointObjList.Add(go);
            go.transform.position = v[v.Count - 1];
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            v.RemoveAt(0);
            vdir.RemoveAt(0);
            Destroy(pointObjList[0]);
            pointObjList.RemoveAt(0);
        }

        foreach (Edge e in g.edges) {
            if (e.start != null && e.end != null) {
                Debug.DrawLine(e.start, e.end);
            }
        }

    }
}
