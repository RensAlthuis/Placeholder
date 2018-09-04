using MapEngine;
using UnityEngine;

public class UnitMovement : MonoBehaviour {
    private Vector3 dest;
    private float startDist;
    private bool move;
    private const float BADHEIGHTFIX = 2f;
    private const float SPEED = 1f;

    void Awake(){
    }

    // Update is called once per frame, so to keep to the same speed, FixedUpdate should be used
    void Update () {
        if (move) {
            transform.position = Vector3.MoveTowards(transform.position, dest, SPEED) * Time.deltaTime;
            if (transform.position == dest) move = false;
        }
	}

	public void MoveTo(TileData tile){ // to slowly move to the tile
        dest = tile.pos + new Vector3(0,BADHEIGHTFIX,0);
        move = true;
		startDist = Vector3.Distance(transform.position, dest);
	}

    public void SpawnTo(TileData tile) { // to instantly move to the selected tile
        transform.position = tile.pos + new Vector3(0, BADHEIGHTFIX, 0);
    }
}
