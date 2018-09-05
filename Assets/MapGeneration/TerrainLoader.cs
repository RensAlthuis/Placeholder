using System.Collections.Generic;
using UnityEngine;

namespace MapEngine {

    public static class TerrainLoader {

        public static TerrainType DEFAULT = new TerrainType(0, "Default", "Materials/Default");
        public static TerrainType WATER = new TerrainType(1, "Water", "Materials/Terrain/Water");
        public static TerrainType LAND = new TerrainType(2, "Grass", "Materials/Terrain/Grass");

        private static Dictionary<TerrainType, Material> mats = new Dictionary<TerrainType, Material>(){
            {DEFAULT, Resources.Load<Material>(DEFAULT.path)}
        };

        public static Material GetMaterial(TerrainType type) {

            if (!mats.ContainsKey(type)){
                mats[type] = Resources.Load<Material>(type.path);
            }
            if(mats[type] == null){
                return mats[DEFAULT];
            }
            return mats[type];
        }

    }
}