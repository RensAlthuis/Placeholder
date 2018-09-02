using System.Collections.Generic;
using UnityEngine;

namespace MapEngine {

    public class TerrainType {

        public static TerrainType WATER = new TerrainType("water"); // I now made extra use of the type! :) it can be read like a string
        public static TerrainType LAND = new TerrainType("land");

        private string type; // no one knows about this. terrains little secret
        private TerrainType (string type) {
            this.type = type;
        }

        private static Dictionary<TerrainType, Material> mats = new Dictionary<TerrainType, Material>() {
            { LAND, Resources.Load<Material>("Materials/Green") },
            { WATER, Resources.Load<Material>("Materials/Blue") }
        };

        public Material GetMaterial() {
            if (!mats.ContainsKey(this)) throw new System.Exception("This type does not exist or is not defined");
            return mats[this];
        }

        public string GetName() {
            return type;
        }
    }
}