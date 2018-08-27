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
        // tiles.isStatic = true;
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


        Material[] matlist =  new Material[]{TerrainType.WATER.GetMaterial(), TerrainType.LAND.GetMaterial()};
        List<MeshFilter> meshList = new List<MeshFilter>();
        tiles.GetComponentsInChildren<MeshFilter>(true, meshList);

        for(int i = 0; i <= meshList.Count / 3000; i++){
            Debug.Log(i);
            Mesh mesh = createSingleMesh(meshList.GetRange(i * 3000,  Mathf.Min(3000, meshList.Count - (i*3000))).ToArray(), tiles.transform.localToWorldMatrix);
            GameObject obj = new GameObject(){ name = "tileMeshPart" };
            obj.transform.SetParent(tiles.transform);
            obj.AddComponent<MeshFilter>().mesh = mesh;
            obj.AddComponent<MeshRenderer>().materials = matlist;
        }
    }

    private float GenerateHeight(float x, float y) {
        float height = Mathf.PerlinNoise(x / lengthX * roughness, y / lengthY * roughness) * heightDifference;
        return (height < SEALEVEL ? SEALEVEL : height);
    }

    private TerrainType GenerateType(float height) {
        return (height == SEALEVEL ? TerrainType.WATER : TerrainType.LAND);
    }

    private List<Vector2f> CreateRandomPoint(Rectf bounds) {
        List<Vector2f> points = new List<Vector2f>();
        for (int i = 0; i < polygonNumber; i++) {
            points.Add(new Vector2f(Random.Range(bounds.left, bounds.right), Random.Range(bounds.bottom, bounds.top)));
        }
        return points;
    }
    private Mesh createSingleMesh(MeshFilter[] meshes, Matrix4x4 transform){
    //combine mesh
        List<CombineInstance> water = new List<CombineInstance>();
        List<CombineInstance> land = new List<CombineInstance>();

        for(int i = 0; i < meshes.Length; i++){
            CombineInstance ci = new CombineInstance();
            MeshRenderer renderer = meshes[i].GetComponent<MeshRenderer>();
            string materialName = renderer.material.name.Replace(" (Instance)", "");
            if(materialName == "Blue"){
                ci.mesh = meshes[i].mesh;
                ci.transform = meshes[i].transform.localToWorldMatrix;
                water.Add(ci);
            }else if(materialName == "Green"){
                ci.mesh = meshes[i].mesh;
                ci.transform = meshes[i].transform.localToWorldMatrix;
                land.Add(ci);
            }
        }

        Mesh combinedWaterMesh = new Mesh();
        combinedWaterMesh.CombineMeshes(water.ToArray());
        Mesh combinedLandMesh = new Mesh();
        combinedLandMesh.CombineMeshes(land.ToArray());
        CombineInstance[] totalmesh = new CombineInstance[2];
        totalmesh[0].mesh = combinedWaterMesh;
        totalmesh[0].transform = transform;
        totalmesh[1].mesh = combinedLandMesh;
        totalmesh[1].transform = transform;

        Mesh finalMesh = new Mesh();

        finalMesh.CombineMeshes(totalmesh, false);
        return finalMesh;
    }
}

