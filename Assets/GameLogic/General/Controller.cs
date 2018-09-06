using UnityEngine;

public class Controller : MonoBehaviour{
    public TileMap tileMap;
    public UI ui;
    private void Start(){

    }

    void Update ()
	{
		if(Input.GetKeyDown(KeyCode.B))
		{
            GameObject obj = Instantiate(UnitLoader.GetPrefab(UnitLoader.RED_BARRON));
            obj.GetComponent<UnitMovement>()?.SpawnTo(tileMap.GetTile(0));
        }
    }

}