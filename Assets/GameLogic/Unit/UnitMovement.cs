using MapEngine;
using UnityEngine;

public class UnitMovement : MonoBehaviour {
    private Vector3 dest;
    private float startDist;
    private bool move;
    private const float SPEED = 1f;

    void Update () {
        if (move) {
            transform.position = Vector3.MoveTowards(transform.position, dest, SPEED) * Time.deltaTime;
            if (transform.position == dest) move = false;
        }
	}

	public void MoveTo(TileData tile){ // to slowly move to the tile
        dest = tile.transform.position;
        move = true;
		startDist = Vector3.Distance(transform.position, dest);
	}

    public void SpawnTo(TileData tile) { // to instantly move to the selected tile
        transform.position = tile.transform.position;
    }
}
