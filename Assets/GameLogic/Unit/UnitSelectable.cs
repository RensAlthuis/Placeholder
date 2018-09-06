using UnityEngine;


[RequireComponent(typeof(MeshCollider))]
public class UnitSelectable : MonoBehaviour, ISelectable{

    private bool selected;

    public void Select() { // when the unit is selected
        selected = true;
    }

    public void Deselect() { // when the unit is deselected
        selected = false;
    }

    public string Name()
    {
        return name;
    }

}