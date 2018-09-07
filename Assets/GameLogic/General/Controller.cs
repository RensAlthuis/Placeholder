using UnityEngine;

public class Controller : MonoBehaviour{
    public UI ui;

    private TileController tileController;
    private void Start(){
        tileController = FindObjectOfType<TileController>();
    }

    void Update ()
	{
		if(Input.GetKeyDown(KeyCode.B))
		{
            GameObject obj = Instantiate(UnitLoader.GetPrefab(UnitLoader.RED_BARRON));
            obj.GetComponent<UnitMovement>()?.SpawnTo(tileController.GetTile(0));
        }
    }

}