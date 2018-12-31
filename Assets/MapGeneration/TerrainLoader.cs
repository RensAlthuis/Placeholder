using System.Collections.Generic;
using UnityEngine;

namespace MapEngine {

    [CreateAssetMenu(menuName = "Custom/TerrainType")]
    public class TerrainType : ScriptableObject {
        [SerializeField] private bool isLand;
        public bool IsLand { get { return isLand; } }
        public Material material;
    }

    public class TerrainLoader {
        public static TerrainType DEFAULT = Resources.Load<TerrainType>("ScriptableObjects/Terrain/Land"); // TODO: find a good default value (something errory? like pink?)
        public static TerrainType WATER = Resources.Load<TerrainType>("ScriptableObjects/Terrain/Water");
        public static TerrainType LAND = Resources.Load<TerrainType>("ScriptableObjects/Terrain/Land");
    }
}