using System.Collections.Generic;
using UnityEngine;

namespace MapEngine {

    [CreateAssetMenu(menuName = "Custom/UnitType")]
    public class UnitType : ScriptableObject {
        public int id;
        public GameObject Object;
    }

    //===================================================================//

    public static class UnitLoader {
        // get ready for a LONG list of units
        public static UnitType RED_BARRON = Resources.Load<UnitType>("ScriptableObjects/Units/RedBarron");
        public static UnitType MISTER_C = Resources.Load<UnitType>("ScriptableObjects/Units/MisterC");
        //...
        //...
    }
}