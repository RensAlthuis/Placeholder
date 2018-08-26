using System.Collections.Generic;
using UnityEngine;

public class TerrainTypes{

    public enum Type{WATER, LAND}

    private static Dictionary<Type, Material> mats;
    public static void preloadMaterials(){
            mats = new Dictionary<Type, Material>();
            mats[Type.LAND] = Resources.Load<Material>("Materials/Green");
            mats[Type.WATER] = Resources.Load<Material>("Materials/Blue");
    }

    public static Material GetMaterial(Type type){
        if(!mats.ContainsKey(type)){
            return mats[Type.LAND];
        }
        return mats[type];
    }
}