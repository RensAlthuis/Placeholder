using System.Collections.Generic;
using UnityEngine;
using csDelaunay;
using MapGraphics;

public static class MapGenerator {

    // CONSTANTS
    private static int RELAXATION = 2;

    private static Rectf bounds;
    private static int polygonNumber; // the amount of tiles
    private static int roughness;
    private static int heightDifference; // maybe both make these constants ?
    private static int SEALEVEL;

    public static void setOptions(int lengthX, int lengthY, int polygonNumber, int roughness, int heightDifference){
        MapGenerator.bounds = new Rectf(0, 0, lengthX, lengthY);
        MapGenerator.polygonNumber = polygonNumber;
        MapGenerator.roughness = (roughness == 0 ? 1 : roughness); //TODO: prolly change this to something more logical
        MapGenerator.heightDifference = heightDifference;
        MapGenerator.SEALEVEL = heightDifference/2;
    }

    public static Tile[] NewMap(TileMap tileMap)
    {
        Tile[] tileList = new Tile[polygonNumber]; // we will definitely need this at some point

        // 1) Creating points
        List<Vector2f> points = CreateRandomPoint(bounds);

        // 2) Creating actual voronoi diagram, with lloyd relaxation thingies
        Voronoi voronoi = new Voronoi(points, bounds, RELAXATION);

        // 3) Creating tiles
        foreach (Site s in voronoi.SitesIndexedByLocation.Values){
            float height = GenerateHeight(s.x, s.y);
            TerrainType type = GenerateType(height);

            TileObject tObj = TileObject.Create(tileMap, s, height, type.GetMaterial(), bounds);
            Tile tile = new Tile(tileMap, tObj, type);
            tileList[s.SiteIndex] = tile;
            s.tile = tile;
        }

        //damn this is ugly :c
        foreach(Site s in voronoi.SitesIndexedByLocation.Values){
            s.tile.neighbors = s.getNeighbourTiles();
        }

        // 4) Creating edges
        GameObject edges = new GameObject() { name = "Edges" };
        foreach (EdgeDelaunay e in voronoi.Edges) {
            if(!e.Visible()) continue;
            new Edge(edges, e);
        }

        return tileList;
    }

    private static float GenerateHeight(float x, float y) {
        float height = Mathf.PerlinNoise(x / bounds.width * roughness, y / bounds.height * roughness) * heightDifference;
        return (height < SEALEVEL ? SEALEVEL : height);
    }

    private static TerrainType GenerateType(float height) {
        return (height == SEALEVEL ? TerrainType.WATER : TerrainType.LAND);
    }

    private static List<Vector2f> CreateRandomPoint(Rectf bounds) {
        List<Vector2f> points = new List<Vector2f>();
        for (int i = 0; i < polygonNumber; i++) { points.Add(new Vector2f(Random.Range(bounds.left, bounds.right), Random.Range(bounds.bottom, bounds.top))); }
        return points;
    }
}

