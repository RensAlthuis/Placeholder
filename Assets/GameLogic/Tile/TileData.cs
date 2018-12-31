using MapEngine;
using System.Collections.Generic;
using UnityEngine;

// Holds the Tile data

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class TileData : MonoBehaviour {
    private List<Edge> edges = new List<Edge>();
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    public TerrainType type { get; private set; }
    public TileData[] neighbors{get; private set;}
    public int index { get; private set; }

    //===================================================================/

    private void Awake() {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void init(TileMap tileMap, int index, Vector3 pos, Mesh mesh, TerrainType type) {
        name = "Tile " + index;
        transform.position = pos;
        this.type = type;
        this.index = index;

        // Setting up the mesh
        meshFilter.mesh = mesh;
        meshRenderer.material = type.material;

        if (type.Equals(TerrainLoader.WATER)) {
            meshRenderer.receiveShadows = false;
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

    public void addEdge(Edge e) { edges.Add(e); }
    public void setNeighbours(TileData[] neighbors){ this.neighbors = neighbors; }
}