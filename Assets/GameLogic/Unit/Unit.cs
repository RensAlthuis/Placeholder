using MapEngine;

public class Unit : Selectable {

    private UnitObject unitObj;
    private MainController tileMap;

    private Tile tile; // the tile at which the unit is currently standing
    private UnitType type; // the type of unit

    public Unit(MainController tileMap, Tile tile, UnitType type) {
        this.tileMap = tileMap;
        this.tile = tile;
        this.type = type;
        unitObj = UnitObject.Create(this, type);
        unitObj.SpawnTo(tile);
    }

    // ========================= INTERACTION ==============================

    private bool selected;
    public bool isSelected { get { return selected; } } // unnecessary

    public void Select() {
        if(tileMap.SetSelected(this)) {
            selected = true;
            unitObj.SetSelected();
        }
    }

    public bool Deselect(Selectable obj) { // Made it so that selecting a tile while having selected a unit does not deselect the unit
        if (obj is Tile) {
            tile = (obj as Tile);
            tile.Highlight();
            unitObj.MoveTo(tile);
            return false;
        } else {
            selected = false;
            unitObj.SetDeselected();
            return true;
        }
    }

    public void Highlight() {
        
    }

    public string Name() {
        return type.GetName();
    }
}
