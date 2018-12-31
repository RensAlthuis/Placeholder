using MapEngine;
using System.Collections.Generic;
using UnityEngine;

// Holds the Tile data

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Tile : MonoBehaviour {

    private List<Edge> edges = new List<Edge>();
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    public TerrainType type { get; private set; }
    public Tile[] neighbors{get; private set;}
    public int index { get; private set; }

    //===================================================================//

    private void Awake() {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }

    public void init(int index, Vector3 pos, Mesh mesh, TerrainType type) {
        name = "Tile " + index;
        transform.position = pos;
        this.type = type;
        this.index = index;

        setMesh(mesh);
    }

    private void setMesh(Mesh mesh) {
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
        meshRenderer.material = type.material;

        if (type.Equals(TerrainLoader.WATER)) {
            meshRenderer.receiveShadows = false;
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

    //===================================================================//

    public void addEdge(Edge e) { edges.Add(e); }
    public void setNeighbours(Tile[] neighbors){ this.neighbors = neighbors; }
}