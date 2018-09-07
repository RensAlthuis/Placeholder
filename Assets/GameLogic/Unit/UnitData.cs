using MapEngine;
using UnityEngine;

public class UnitData : MonoBehaviour{

    public TileData currentTile;
    [SerializeField] private UnitType type;
    private bool selected;

    void Awake(){
    }
}
