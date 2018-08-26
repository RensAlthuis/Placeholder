using System.Collections.Generic;
using UnityEngine;
using csDelaunay;



public class MapGenerator {

    // CONSTANTS
    private static int RELAXATION = 2;

    private Material mat; // TEMP the default material used on all the tiles
    private Material water;
    private int lenghtX;
    private int lengthY;
    private int polygonNumber; // the rough amount of tiles
    private int roughness;
    private int heightDifference; // maybe both make these constants ?
    private int SEALEVEL;

    public MapGenerator(Material mat, Material water, int width, int height, int polygonNumber, int roughness, int heightDifference)
    {
        this.mat = mat;
        this.water = water;
        lenghtX = width;
        lengthY = height;
        this.polygonNumber = polygonNumber;
        this.roughness = (roughness == 0 ? 1 : roughness); //TODO: prolly change this so something more logical
        this.heightDifference = heightDifference;
        SEALEVEL = heightDifference / 2;
    }

    public void newMap()
    {
        // 1) Creating points
        Rectf bounds = new Rectf(0, 0, lenghtX, lengthY);
        List<Vector2f> points = CreateRandomPoint(bounds);

        // 2) Creating actual voronoi diagram, with lloyd relaxation thingies
        Voronoi voronoi = new Voronoi(points, bounds, RELAXATION);

        // 3) Creating tiles
        GameObject tiles = new GameObject() { name = "Tiles" };
        foreach (Site s in voronoi.SitesIndexedByLocation.Values){
            float height = Mathf.PerlinNoise(s.x/roughness, s.y/roughness) * heightDifference;
            height = (height < SEALEVEL ? SEALEVEL : height);

            // Generate the tileMesh
            TileMesh tileMesh = new TileMesh(tiles,
                                             s.SiteIndex,
                                             new Vector3(s.x, height, s.y),
                                             s.Region(bounds).ConvertAll(x => new Vector3(x.x - s.x, 0, x.y - s.y)).ToArray(),
                                             30
                                            );

            Tile tile = new Tile(s, tileMesh, height == SEALEVEL ? TerrainTypes.Type.WATER : TerrainTypes.Type.LAND);

        }

        // 4) Creating edges
        GameObject edges = new GameObject() { name = "Edges" }; // TODO: find a clever way to link tiles and edges so that no duplicate edges are created
        foreach (EdgeDelaunay e in voronoi.Edges) {
            Edge edge = new Edge(edges, e);
        }
    }

    private List<Vector2f> CreateRandomPoint(Rectf bounds) {
        List<Vector2f> points = new List<Vector2f>();
        for (int i = 0; i < polygonNumber; i++) {
            points.Add(new Vector2f(Random.Range(bounds.left, bounds.right), Random.Range(bounds.bottom, bounds.top)));
        }
        return points;
    }
}

