using System.Collections.Generic;
using UnityEngine;
using csDelaunay;


public class MapGenerator
{
    // CONSTANTS
    private static int RELAXATION = 2;

    private Material mat; // TEMP the default material used on all the tiles
    private int lenghtX;
    private int lengthY;
    private int polygonNumber; // the rough amount of tiles

    private Dictionary<Vector2f, Site> sites;
    private GameObject pointcontainer;

    public MapGenerator(Material mat, int width, int height, int polygonNumber)
    {
        this.mat = mat;
        lenghtX = width;
        lengthY = height;
        this.polygonNumber = polygonNumber;
    }

    public void newMap()
    {
        // 1) Creating points
        Rectf bounds = new Rectf(0, 0, lenghtX, lengthY);
        List<Vector2f> points = CreateRandomPoint(bounds);


        // 2) Creating actual voronoi diagram, with lloyd relaxation thingies
        Voronoi voronoi = new Voronoi(points, bounds, RELAXATION);
        sites = voronoi.SitesIndexedByLocation;


        // 3) Create Unity stuff
        GameObject tiles = new GameObject() { name = "Tiles" };
        foreach (Site s in sites.Values){
            Tile tile = new Tile(tiles,s,bounds);
            tile.obj.GetComponent<MeshRenderer>().material = mat; // TEMP should be decided inside Tile
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

