using UnityEngine;
using MapEngine;

public interface Selectable { // All things that could be selected
    void Select(); // when clicking on a unit/tile
    bool Deselect(Selectable newSelection); // when clicking on another unit/tile
    void Highlight(); // when a unit/tile can be selected (like neighbouring tiles)
}

public class MapController : MonoBehaviour {

    private Tile[] tiles;

    public int lengthX = 2000;
    public int lengthY = 2000;
    public int polygonNumber = 2000;
    public int roughness = 5;
    public int heightDifference = 10;

    public Selectable selected;

    public void Start() {
        tiles = MapGenerator.NewMap(this, lengthX, lengthY, polygonNumber, roughness, heightDifference);
        new Unit(this, tiles[0], UnitType.RED_BARRON);
        new Unit(this, tiles[1], UnitType.MISTER_C);
        new Unit(this, tiles[2], UnitType.MISTER_C);
    }

    internal bool SetSelected(Selectable obj) { // governs the selected and previous-selected objected
        if(selected != null) if(!selected.Deselect(obj)) return false; // feedback to the previous-selected object
        selected = obj;
        return true;
    }

    // maybe all global graphical map stuff also here? like fog and random birds
}
