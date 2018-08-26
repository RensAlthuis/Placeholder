using csDelaunay;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    private List<Site> neighbors;
    private List<Edge> edges;

    private TileMesh tileMesh;
    private int height;
    private Site s;

    TerrainTypes.Type type;

    public Tile (Site s, TileMesh tileMesh, TerrainTypes.Type type) {
        //neighbors = s.NeighborSites(); // TODO: why does this not work?
        this.tileMesh = tileMesh;
        this.s = s;
        this.type = type;
        tileMesh.setMaterial(TerrainTypes.GetMaterial(type));
    }
}
