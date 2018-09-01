using UnityEngine;

public class UnitObject : MonoBehaviour {

    // CONSTANTS // Use this for initialization
    private const float SPEED = 10;

    //public Vector3 dest;
	//private float startDist;

    public static Unit Create() { // WORK IN PROGRESS   
        GameObject obj = new GameObject();
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshCollider>();
        obj.AddComponent<MeshRenderer>();
        return obj.AddComponent<TileObject>().Init(tile, s, height, type, bounds, transformMap);
    }

    // Update is called once per frame
    void Update () {
		//smooth tile movement
		if (dest != Vector3.zero){
			float distance = Vector3.Distance(transform.position, dest);
			float speed = Mathf.Lerp(0.1f, SPEED, SPEED*(distance/startDist));
			transform.position = Vector3.MoveTowards(transform.position, dest, speed);
			if(distance < 1){dest = Vector3.zero;};
		}
	}

	public void moveToTile(TileObject tObj){
		dest = new Vector3(tObj.transform.position.x, tObj.transform.position.y + tObj.transform.localScale.y,tObj.transform.position.z);
		startDist = Vector3.Distance(transform.position, dest);
	}
}
