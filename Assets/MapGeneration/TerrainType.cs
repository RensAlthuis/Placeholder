using System.Collections.Generic;
using UnityEngine;

namespace MapGraphics {

    public class TerrainType {

        public static TerrainType WATER = new TerrainType(0);
        public static TerrainType LAND = new TerrainType(1);
        
        private int type; // no one knows about this. terrains little secret
        private TerrainType (int type) {
            this.type = type;
        }

        private static Dictionary<TerrainType, Material> mats = new Dictionary<TerrainType, Material>() {
            { LAND, Resources.Load<Material>("Materials/Green") },
            { WATER, Resources.Load<Material>("Materials/Blue") }
        };

        public Material GetMaterial() {
            if (!mats.ContainsKey(this)) {
                return mats[LAND];
            }
            return mats[this];
        }
    }
}