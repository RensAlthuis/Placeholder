using MapEngine;
using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TileData : MonoBehaviour{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    public TerrainType type;
    private List<Edge> edges;

    public TileMap tileMap{get; private set;}
    public GameObject[] neighbors{get; private set;}
    public Vector3 pos{get; private set;}
    public int index {get; private set;}

    public void Awake(){
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Init(TileMap tileMap, int index, Vector3 pos, Mesh mesh, TerrainType type) {
        this.tileMap = tileMap;
        this.index = index;
        this.pos = pos;
        this.transform.position = pos;
        this.name = index + ":  " + type.name;
        this.type = type;
        edges = new List<Edge>();
        meshFilter.mesh = mesh;
        meshRenderer.material = TerrainLoader.GetMaterial(type);
    }

    public void addEdge(Edge e){
        edges.Add(e);
    }

    public void setNeighbours(GameObject[] neighbors){
        this.neighbors = neighbors;
    }
}