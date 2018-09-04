using UnityEngine;


public class UnitSelectable : Selectable{
    UnitMovement unitMovement;

    protected override void Awake(){
        base.Awake();
    }
    public override void Select() { // when the unit is selected
        selected = true;
    }

    public override void Deselect() { // when the unit is deselected
        selected = true;
    }

    public override string Name()
    {
        return name;
    }

}