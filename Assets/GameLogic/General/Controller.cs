using UnityEngine;

public class Controller : MonoBehaviour{
    public TileMap tileMap;
    public UI ui;
    public GameObject unitPrefab;

    private void Start(){
        // GameObject obj = Instantiate(unitPrefab);
        // obj.GetComponent<UnitData>().Init(tileMap.GetTile(1), UnitType.RED_BARRON);
        // obj = Instantiate(unitPrefab);
        // obj.GetComponent<UnitData>().Init(tileMap.GetTile(1), UnitType.MISTER_C);
        // obj = Instantiate(unitPrefab);
        // obj.GetComponent<UnitData>().Init(tileMap.GetTile(2), UnitType.MISTER_C);
    }
}