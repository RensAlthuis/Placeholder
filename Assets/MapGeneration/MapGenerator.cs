using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

namespace MapEngine {

    public static class MapGenerator {

        // CONSTANTS
        private static int RELAXATION = 2;

        public static TileData[] NewMap(TileMap tileMap, int lengthX, int lengthY, int polygonNumber, int roughness, int heightDifference, GameObject tilePrefab) {
            Rectf bounds = new Rectf(0, 0, lengthX, lengthY);
            int SEALEVEL = heightDifference / 2;

            // 1) Creating points
            List<Vector2f> points = CreateRandomPoint(bounds, polygonNumber);

            // 2) Creating actual voronoi diagram, with lloyd relaxation thingies
            Voronoi voronoi = new Voronoi(points, bounds, RELAXATION);

            // 3) Creating tiles
            TileData[] tileArray = new TileData[polygonNumber]; // TILEARRAY
            foreach (Site s in voronoi.SitesIndexedByLocation.Values) {
                //create properties
                float height = GenerateHeight(s.x, s.y, bounds, (roughness <= 0 ? 1 : roughness), heightDifference, SEALEVEL); //TODO: prolly change this to something more logical
                TerrainType type = null;//GenerateType(height, SEALEVEL);
                Vector3[] hull = s.Region(bounds).ConvertAll(x => new Vector3(x.x - s.x, 0, x.y - s.y)).ToArray();
                Vector3 pos = new Vector3(s.x, height, s.y);
                Mesh mesh = TileMesh.Create(hull);

                //initialise Tile
                GameObject tileObj = GameObject.Instantiate(tilePrefab, tileMap.transform);
                tileObj.tag = "Tile";
                tileArray[s.SiteIndex] = tileObj.GetComponent<TileData>();
                tileArray[s.SiteIndex].Init(tileMap, s.SiteIndex, pos, mesh, type); // TILEARRAY
                if(type.Equals(TerrainLoader.WATER)){
                    tileObj.GetComponent<MeshRenderer>().receiveShadows = false;
                    tileObj.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }
                tileObj.GetComponent<MeshCollider>().sharedMesh = mesh;
                s.tile = tileArray[s.SiteIndex]; //ugly stuff
            }

            GameObject[] objects = GameObject.FindGameObjectsWithTag("Tile");
            StaticBatchingUtility.Combine(objects, tileMap.gameObject);
            // 4) Creating edges
            GameObject edges = new GameObject() { name = "Edges" };
            foreach (EdgeDelaunay e in voronoi.Edges) {
                if (!e.Visible()) continue;
                new Edge(edges, e);
            }

            // this has to be here because all tiles need to exist before we can assign neighbours
            // it's ugly but only takes a couple miliseconds so.. oh well
            foreach(Site s in voronoi.SitesIndexedByLocation.Values){
                s.tile.setNeighbours(s.getNeighbourTiles());
            }

            return tileArray; // TILEARRAY
        }

        private static float GenerateHeight(float x, float y, Rectf bounds, int roughness, int heightDifference, int SEALEVEL) {
            float height = Mathf.PerlinNoise(x / bounds.width * roughness, y / bounds.height * roughness) * heightDifference;
            return (height < SEALEVEL ? SEALEVEL : height);
        }

        private static TerrainLoader GenerateType(float height, int SEALEVEL) {
            return (height == SEALEVEL ? TerrainLoader.WATER : TerrainLoader.LAND);
        }

        private static List<Vector2f> CreateRandomPoint(Rectf bounds, int polygonNumber) {
            List<Vector2f> points = new List<Vector2f>();
            for (int i = 0; i < polygonNumber; i++) { points.Add(new Vector2f(Random.Range(bounds.left, bounds.right), Random.Range(bounds.bottom, bounds.top))); }
            return points;
        }
    }
}

