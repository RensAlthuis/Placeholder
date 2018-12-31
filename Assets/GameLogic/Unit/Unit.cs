using MapEngine;
using UnityEngine;

public class Unit : MonoBehaviour{

    private Tile currentTile;
    [SerializeField] private UnitType type;
    private bool selected;

    void Awake(){
    }
}
