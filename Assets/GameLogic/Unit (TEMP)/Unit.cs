using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public Vector3 dest;
	private float startDist;
	// Use this for initialization
	private const float SPEED = 10;
	void Start () {

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
