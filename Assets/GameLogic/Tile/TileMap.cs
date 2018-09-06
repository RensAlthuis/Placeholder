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

    public void Start(){
        tiles = MapGenerator.NewMap(this, lengthX, lengthY, amountTiles, roughness, heightDifference, tilePrefab);
        SelectableController.Click += SetSelected;
    }

    public void SetSelected(ISelectable obj) { // governs the selected and previous-selected objected
        if(SelectableController.current != null){
            SelectableController.current.Deselect();
        }

        if(obj == null) return;
        if(! (obj is TileSelectable)) return;

        SelectableController.current = obj;
        obj.Select();
    }

    public void DeselectAll() {
        if (Input.GetKeyDown(UNSELECT)) SetSelected(null);
    }

    public TileData GetTile(int index){
        return tiles[index];
    }
}
