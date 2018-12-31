using UnityEngine;

public interface ISelectable {
    void Select(); // when clicking on a unit/tile
    void Deselect(); // when clicking on another unit/tile
    string Name(); // the name of the object
}

//===================================================================//

// TODO: mesh TileSelectable and UnitSelectable to abstract class?

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Tile))]
[RequireComponent(typeof(MeshCollider))]
public class TileSelectable : MonoBehaviour, ISelectable {

    private MeshRenderer meshRenderer;
    private Tile tileData;
    private bool selected;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        tileData = GetComponent<Tile>();
    }

    public string Name() {
        return name;
    }

    public void Select() {
        selected = true;
        HighLight(true);
    }

    public void Deselect() {
        selected = false;
        HighLight(false);
    }

    private void HighLight(bool on) {
        if (on) {
            float H, S, V;
            Color.RGBToHSV(tileData.type.material.color, out H, out S, out V);
            meshRenderer.material.color = Color.HSVToRGB(H, S, 1);
        } else {
            meshRenderer.material = tileData.type.material;
        }
    }
}

//===================================================================//

[RequireComponent(typeof(MeshCollider))]
public class UnitSelectable : MonoBehaviour, ISelectable {

    private bool selected;

    public void Select() { // when the unit is selected
        selected = true;
    }

    public void Deselect() { // when the unit is deselected
        selected = false;
    }

    public string Name() {
        return name;
    }

}