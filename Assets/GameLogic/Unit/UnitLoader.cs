using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UnitType{
    public int id;
    public string name;
    public string path;
}

public static class UnitLoader {

    // get ready for a LONG list of units
    public static UnitType MISTER_C = new UnitType(){id = 0, name="Mister C", path="Units/MisterC"};
    public static UnitType RED_BARRON= new UnitType(){id = 1, name="Red Barron", path="Units/RedBarron"};

    private static Dictionary<UnitType, GameObject> objs = new Dictionary<UnitType, GameObject>(){
        {MISTER_C, Resources.Load<GameObject>(MISTER_C.path)}
    };

    public static GameObject GetPrefab(UnitType type) {
        if (!objs.ContainsKey(type)){
            objs[type] = Resources.Load<GameObject>(type.path);
        }
        if (objs[type] == null){
            return objs[MISTER_C];
        }
        return objs[type];
    }
}