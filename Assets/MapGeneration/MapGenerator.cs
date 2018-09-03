using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

namespace MapEngine {

    public static class MapGenerator {

        // CONSTANTS
        private static int RELAXATION = 2;

        public static Tile[] NewMap(MainController tileMap, int lengthX, int lengthY, int polygonNumber, int roughness, int heightDifference) {
            Rectf bounds = new Rectf(0, 0, lengthX, lengthY);
            int SEALEVEL = heightDifference / 2;

            // 1) Creating points
            List<Vector2f> points = CreateRandomPoint(bounds, polygonNumber);

            // 2) Creating actual voronoi diagram, with lloyd relaxation thingies
            Voronoi voronoi = new Voronoi(points, bounds, RELAXATION);

            // 3) Creating tiles
            GameObject tiles = new GameObject() { name = "Tiles" };
            Tile[] tileArray = new Tile[polygonNumber]; // TILEARRAY
            foreach (Site s in voronoi.SitesIndexedByLocation.Values) {
                float height = GenerateHeight(s.x, s.y, bounds, (roughness <= 0 ? 1 : roughness), heightDifference, SEALEVEL); //TODO: prolly change this to something more logical
                TerrainType type = GenerateType(height, SEALEVEL);
                tileArray[s.SiteIndex] = new Tile(tileMap, s, height, type, bounds, tiles.transform); // TILEARRAY
            }

            // 4) Creating edges
            GameObject edges = new GameObject() { name = "Edges" };
            foreach (EdgeDelaunay e in voronoi.Edges) {
                if (!e.Visible()) continue;
                new Edge(edges, e);
            }

            return tileArray; // TILEARRAY
        }

        private static float GenerateHeight(float x, float y, Rectf bounds, int roughness, int heightDifference, int SEALEVEL) {
            float height = Mathf.PerlinNoise(x / bounds.width * roughness, y / bounds.height * roughness) * heightDifference;
            return (height < SEALEVEL ? SEALEVEL : height);
        }

        private static TerrainType GenerateType(float height, int SEALEVEL) {
            return (height == SEALEVEL ? TerrainType.WATER : TerrainType.LAND);
        }

        private static List<Vector2f> CreateRandomPoint(Rectf bounds, int polygonNumber) {
            List<Vector2f> points = new List<Vector2f>();
            for (int i = 0; i < polygonNumber; i++) { points.Add(new Vector2f(Random.Range(bounds.left, bounds.right), Random.Range(bounds.bottom, bounds.top))); }
            return points;
        }
    }
}

