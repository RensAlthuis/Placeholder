using System.Collections.Generic;
using UnityEngine;

namespace MapEngine {

    [CreateAssetMenu(menuName = "Custom/TerrainType")]
    public class TerrainType : ScriptableObject
    {
        [SerializeField] private bool isLand;
        public bool IsLand { get { return isLand; } }
        public Material material;
    }

    public class TerrainLoader {

        public static TerrainType DEFAULT= Resources.Load<TerrainType>("Default");
        public static TerrainType WATER = Resources.Load<TerrainType>("Water");
        public static TerrainType LAND = Resources.Load<TerrainType>("Land");

    }
}