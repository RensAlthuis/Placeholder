using UnityEngine;

public class UnitController : MonoBehaviour{

    private bool curIsUnit;
    private void Start(){
        SelectableController.UpdateSelected += SelectedUpdate;
        MouseController m = FindObjectOfType<MouseController>();
        if (m != null)
            m.rightClick += Move;
    }

    private void SelectedUpdate(ISelectable selectable){
        curIsUnit = (selectable is UnitSelectable);
    }

    private void Move(GameObject obj){
        if(!curIsUnit) return;

        TileData td = obj.GetComponent<TileData>();
        if(td == null) return;

        UnitMovement um = (SelectableController.current as UnitSelectable).GetComponent<UnitMovement>();
        um.SpawnTo(td);
    }
}