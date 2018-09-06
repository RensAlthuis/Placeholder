using MapEngine;
using UnityEngine;

public class UnitData : MonoBehaviour{

    private TileData currentTile;
    [SerializeField] private UnitType type;
    private bool selected;

    void Awake(){
    }
}
