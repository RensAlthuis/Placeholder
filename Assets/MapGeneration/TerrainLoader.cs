using System.Collections.Generic;
using UnityEngine;

namespace MapEngine {

    [CreateAssetMenu(menuName = "hoi")]
    public class TerrainType : ScriptableObject
    {
        [SerializeField] private bool isLand;
        public bool IsLand { get { return isLand; } }

        public Material material;
        public bool test;
    }

    public class TerrainLoader {

        private int id;
        private TerrainLoader(int id) {
            this.id = id;
        }

        public static TerrainLoader DEFAULT = new TerrainLoader(0);
        public static TerrainLoader WATER = new TerrainLoader(0);
        public static TerrainLoader LAND = new TerrainLoader(0);

        /*
        //public static TerrainLoader WATER = new TerrainType("Water", "Materials/Terrain/Water");
        //public static TerrainLoader LAND = new TerrainType("Grass", "Materials/Terrain/Grass");

        private static Dictionary<TerrainType, Material> mats = new Dictionary<TerrainType, Material>(){
            {DEFAULT, Resources.Load<Material>(DEFAULT.path)}
        };*/

        public static Material GetMaterial(TerrainType type) {
            return null;            
        }

    }
}