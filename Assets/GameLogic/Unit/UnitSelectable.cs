using UnityEngine;


public class UnitSelectable : Selectable{

    public override void Select() { // when the unit is selected
        if(Selectable.current != null){
            Selectable.current.Deselect();
        }
        selected = true;
        Selectable.current = this;
    }

    public override void Deselect() { // when the unit is deselected
        selected = false;
    }

    public override string Name()
    {
        return name;
    }

}