using UnityEngine;
using MapEngine;
using UnityEngine.UI;

public class TileMap : MonoBehaviour{

    private static KeyCode UNSELECT = KeyCode.Escape;
    [SerializeField] private GameObject tilePrefab;

    private TileData[] tiles;

    public int lengthX = 200;
    public int lengthY = 200;
    public int amountTiles = 2000;
    public int roughness = 5;
    public int heightDifference = 10;

    private Selectable selected;

    public void Start(){
        tiles = MapGenerator.NewMap(this, lengthX, lengthY, amountTiles, roughness, heightDifference, tilePrefab);
        Selectable.Click += SetSelected;
    }

    public void SetSelected(Selectable obj) { // governs the selected and previous-selected objected
        if(obj.GetType() != typeof(TileSelectable)) return;
        //deselect current selection
        if(selected != null){
            selected.Deselect();
        }

        //set new selection if given
        if(obj != null){
            selected = obj;
            obj.Select();
        }
    }

    public void DeselectAll() {
        if (Input.GetKeyDown(UNSELECT)) SetSelected(null);
    }

    public TileData GetTile(int index){
        return tiles[index];
    }
}
