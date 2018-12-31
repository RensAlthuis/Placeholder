﻿using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

namespace MapEngine {

    public static class MapGenerator {

        // CONSTANTS

        private static int MIN_ROUGHNESS = 1;              // The minimum roughness
        private static int RELAXATION = 2;                 // The relacation is determined to be 2
        private static int SEALEVEL(int h) { return h/2; } // Sealevel is determined to be half of the total height

        //===================================================================//

        public static Tile[] NewMap(TileMap tileMap, int lengthX, int lengthY, int polygonNumber, int roughness, int heightDifference, GameObject tilePrefab) {
            Rectf bounds = new Rectf(0, 0, lengthX, lengthY);

            // 1) Creating points
            List<Vector2f> points = CreateRandomPoint(bounds, polygonNumber);

            // 2) Creating actual voronoi diagram, with lloyd relaxation thingies
            Voronoi voronoi = new Voronoi(points, bounds, RELAXATION);

            // 3) Creating tiles
            Tile[] tileArray = new Tile[polygonNumber];
            foreach (Site s in voronoi.SitesIndexedByLocation.Values) {

                // 3.1) Create properties
                float height = GenerateHeight(s.x, s.y, bounds, (roughness <= 0 ? MIN_ROUGHNESS : roughness), heightDifference, SEALEVEL(heightDifference)); //TODO: prolly change this to something more logical
                TerrainType type = GenerateType(height, SEALEVEL(heightDifference));
                Vector3 pos = new Vector3(s.x, height, s.y);
                Mesh mesh = TileMeshGenerator.Create(s.Region(bounds).ConvertAll(x => new Vector3(x.x - s.x, 0, x.y - s.y)).ToArray());

                // 3.2) Initialising the tiles
                GameObject tileObj = Object.Instantiate(tilePrefab, tileMap.transform);
                tileObj.GetComponent<Tile>().init(s.SiteIndex, pos, mesh, type);
                tileObj.tag = "Tile";

                tileArray[s.SiteIndex] = tileObj.GetComponent<Tile>();
                s.tile = tileObj.GetComponent<Tile>(); // ugly stuff
            } foreach (Site s in voronoi.SitesIndexedByLocation.Values) { // this has to be here because all tiles need to exist before we can assign neighbours
                s.tile.setNeighbours(s.getNeighbourTiles());
            }
            //GameObject[] objects = GameObject.FindGameObjectsWithTag("Tile");
            //StaticBatchingUtility.Combine(objects, tileMap.gameObject);

            // 4) Creating edges
            GameObject edges = new GameObject() { name = "Edges" };
            foreach (EdgeDelaunay e in voronoi.Edges) {
                if (!e.Visible()) continue;
                new Edge(edges, e);
            }

            return tileArray;
        }

        //===================================================================//

        private static float GenerateHeight(float x, float y, Rectf bounds, int roughness, int heightDifference, int sealevel) { // Determines the height of the Tile
            float height = Mathf.PerlinNoise(x / bounds.width * roughness, y / bounds.height * roughness) * heightDifference;
            return (height < sealevel ? sealevel : height);
        }

        private static TerrainType GenerateType(float height, int sealevel) { // Determines the type of the Tile
            return (height == sealevel ? TerrainLoader.WATER : TerrainLoader.LAND);
        }

        private static List<Vector2f> CreateRandomPoint(Rectf bounds, int n) { // Creates n ranodm pouns within the bounds
            List<Vector2f> points = new List<Vector2f>();
            for (int i = 0; i < n; i++) { points.Add(new Vector2f(Random.Range(bounds.left, bounds.right), Random.Range(bounds.bottom, bounds.top))); }
            return points;
        }
    }
}

