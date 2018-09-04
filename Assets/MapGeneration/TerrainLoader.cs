using System.Collections.Generic;
using UnityEngine;

namespace MapEngine {

    public struct TerrainType{
        public int id;
        public string name;
        public string path;
    }


    public static class TerrainLoader {

        public static TerrainType DEFAULT = new TerrainType(){id = 0, name = "Default", path = "Materials/Default"};
        public static TerrainType WATER = new TerrainType(){id = 1, name = "Water", path = "Materials/Terrain/Water"};
        public static TerrainType LAND = new TerrainType(){id = 2, name = "Grass", path = "Materials/Terrain/Grass"};

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