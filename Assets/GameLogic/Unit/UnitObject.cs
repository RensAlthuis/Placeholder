using MapEngine;
using UnityEngine;

public class UnitObject : MonoBehaviour {

    // CONSTANTS // Use this for initialization
    private const float SPEED = 0.1f;
    private const float BADHEIGHTFIX = 2f;

    private Unit unit;

    public static UnitObject Create(Unit unit, UnitType type) {
        return Instantiate(type.GetGameObject()).AddComponent<UnitObject>().Init(unit);
    }

    private UnitObject Init(Unit unit) {
        this.unit = unit;
        return this;
    }

    // ====================== OBJECT INTERACTION =============================

    private void OnMouseDown() { // first to know if it has been clicked
        Debug.Log("Clicked on " + name);
        unit.Select();
    }

    public void SetSelected() { // when the unit is selected
        
    }

    public void SetDeselected() { // when the unit is deselected
        
    }

    // ===================== MOVEMENT ==============================

    private Vector3 dest;
    private float startDist;
    private bool move;

    // Update is called once per frame, so to keep to the same speed, FixedUpdate should be used
    void FixedUpdate () {
        if (move) {
            transform.position = Vector3.MoveTowards(transform.position, dest, SPEED);
            if (transform.position == dest) move = false;
        }
	}

	public void MoveTo(Tile tile){
        dest = tile.Coord + new Vector3(0,BADHEIGHTFIX,0);
        move = true;
		startDist = Vector3.Distance(transform.position, dest);
	}

    public void SpawnTo(Tile tile) {
        transform.position = tile.Coord + new Vector3(0, BADHEIGHTFIX, 0);
    }
}
