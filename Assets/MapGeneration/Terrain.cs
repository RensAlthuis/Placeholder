using System.Collections.Generic;
using UnityEngine;

namespace MapGraphics {

    public class Terrain {

        public static Terrain WATER = new Terrain(0);
        public static Terrain LAND = new Terrain(1);
        
        private int type; // no one knows about this. terrains little secret
        private Terrain (int type) {
            this.type = type;
        }

        private static Dictionary<Terrain, Material> mats = new Dictionary<Terrain, Material>() {
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