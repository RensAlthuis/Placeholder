using UnityEngine;
using MapEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

    private static KeyCode UNSELECT = KeyCode.Escape;

    private Tile[] tiles;
    private UIview UIview;

    [Header("Map Settings")]
    public int lengthX = 2000;
    public int lengthY = 2000;
    public int amountTiles = 2000;
    public int roughness = 5;
    public int heightDifference = 10;

    [Header("UI Settings")]
    public GameObject selectedPanel;
    public Text selectedName;
    public Image selectedImage;

    private Selectable selected;

    public void Start() {
        UIview = new UIview(selectedPanel, selectedName, selectedImage);
        tiles = MapGenerator.NewMap(this, lengthX, lengthY, amountTiles, roughness, heightDifference);
        new Unit(this, tiles[0], UnitType.RED_BARRON);
        new Unit(this, tiles[1], UnitType.MISTER_C);
        new Unit(this, tiles[2], UnitType.MISTER_C);
    }

    public bool SetSelected(Selectable obj) { // governs the selected and previous-selected objected
        if(selected != null) if(!selected.Deselect(obj)) return false; // feedback to the previous-selected object
        selected = obj;
        UIview.Selected(selected);
        return true;
    }

    public void SetUnselected() {
        selected = null;
        UIview.Unselect();
    }

    private void Update() {
        if (Input.GetKeyDown(UNSELECT)) SetUnselected();
    }

    // maybe all global graphical map stuff also here? like fog and random birds
}
