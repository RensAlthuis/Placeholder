using UnityEngine;

public class Controller : MonoBehaviour{
    public TileMap tileMap;
    public UI ui;

    private void Start(){
        GameObject obj = Instantiate(UnitLoader.GetPrefab(UnitLoader.MISTER_C));
        obj.GetComponent<UnitData>().Init(tileMap.GetTile(0));
        obj = Instantiate(UnitLoader.GetPrefab(UnitLoader.RED_BARRON));
        obj.GetComponent<UnitData>().Init(tileMap.GetTile(1));
        obj = Instantiate(UnitLoader.GetPrefab(UnitLoader.RED_BARRON));
        obj.GetComponent<UnitData>().Init(tileMap.GetTile(2));
    }
}