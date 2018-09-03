using System.Collections.Generic;
using UnityEngine;

namespace MapEngine {

    public class UnitType {

        // get ready for a LONG list of units
        public static UnitType RED_BARRON = new UnitType("red barron");
        public static UnitType MISTER_C = new UnitType("mister c");

        private string type; // no one knows about this. units little secret
        private UnitType (string type) {
            this.type = type;
        }

        private static Dictionary<UnitType, GameObject> objs = new Dictionary<UnitType, GameObject>() {
            { RED_BARRON, Resources.Load<GameObject>("Units/RedBarron") },
            { MISTER_C, Resources.Load<GameObject>("Units/MisterC") }
        };

        public GameObject GetGameObject() {
            if (!objs.ContainsKey(this)) throw new System.Exception("This type does not exist or is not defined");
            return objs[this];
        }

        public string GetName() {
            return type;
        }
    }
}