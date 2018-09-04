using MapEngine;
using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class UnitData : MonoBehaviour{

    private TileData currentTile;
    [SerializeField] private UnitType type;
    private UnitMovement unitMovement;
    private bool selected;

    void Awake(){
        unitMovement = GetComponent<UnitMovement>();
    }

    public void Init(TileData currentTile){
        this.currentTile = currentTile;
        unitMovement.SpawnTo(currentTile);
    }
}
