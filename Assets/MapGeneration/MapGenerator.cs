using System.Collections.Generic;
using UnityEngine;
using csDelaunay;
using MapGraphics;

public class MapGenerator {

    // CONSTANTS
    private static int RELAXATION = 2;

    private int lengthX;
    private int lengthY;
    private int polygonNumber; // the amount of tiles
    private int roughness;
    private int heightDifference; // maybe both make these constants ?
    private int SEALEVEL;

    public MapGenerator(int lengthX, int lengthY, int polygonNumber, int roughness, int heightDifference) {
        this.lengthX = lengthX;
        this.lengthY = lengthY;
        this.polygonNumber = polygonNumber;
        this.roughness = (roughness == 0 ? 1 : roughness); //TODO: prolly change this to something more logical
        this.heightDifference = heightDifference;
        SEALEVEL = heightDifference/2;
    }

    public void NewMap()
    {
        // 1) Creating points
        Rectf bounds = new Rectf(0, 0, lengthX, lengthY);
        List<Vector2f> points = CreateRandomPoint(bounds);

        // 2) Creating actual voronoi diagram, with lloyd relaxation thingies
        Voronoi voronoi = new Voronoi(points, bounds, RELAXATION);

        // 3) Creating tiles
        GameObject tiles = new GameObject() { name = "Tiles" };
        foreach (Site s in voronoi.SitesIndexedByLocation.Values){
            float height = GenerateHeight(s.x, s.y);
            new Tile(tiles, s, height, GenerateType(height), bounds);
        }
        tiles.transform.Translate(new Vector3(0, -SEALEVEL, 0), Space.World); // min == SEALEVEL for sealevel occuring once! // also is this really needed?

        // 4) Creating edges
        GameObject edges = new GameObject() { name = "Edges" };
        foreach (EdgeDelaunay e in voronoi.Edges) {
            if(!e.Visible()) continue;
            new Edge(edges, e);
        }
    }

    private float GenerateHeight(float x, float y) {
        float height = Mathf.PerlinNoise(x / lengthX * roughness, y / lengthY * roughness) * heightDifference;
        return (height < SEALEVEL ? SEALEVEL : height);
    }

    private MapGraphics.Terrain GenerateType(float height) {
        return (height == SEALEVEL ? MapGraphics.Terrain.WATER : MapGraphics.Terrain.LAND);
    }

    private List<Vector2f> CreateRandomPoint(Rectf bounds) {
        List<Vector2f> points = new List<Vector2f>();
        for (int i = 0; i < polygonNumber; i++) {
            points.Add(new Vector2f(Random.Range(bounds.left, bounds.right), Random.Range(bounds.bottom, bounds.top)));
        }
        return points;
    }
}

