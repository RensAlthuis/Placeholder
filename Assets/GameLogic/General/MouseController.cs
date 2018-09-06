using UnityEngine;

public class MouseController : MonoBehaviour{

    public delegate void LeftClick(GameObject obj);
    public event LeftClick leftClick = delegate {};

    public delegate void RightClick(GameObject obj);
    public event RightClick rightClick = delegate {};

    private Camera mainCamera;

    private void Start(){
        mainCamera = Camera.main; // this is a GetObjectByTag() call, should be cached for performance reasons
    }

    private void Update(){
        if (Input.GetMouseButtonDown(0))
            leftClick(GetClickedObject());
        if(Input.GetMouseButtonDown(1))
            rightClick(GetClickedObject());
    }

    private GameObject GetClickedObject(){
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        GameObject obj = hit.collider?.gameObject;
        return obj;
    }
}