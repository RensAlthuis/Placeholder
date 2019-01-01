using UnityEngine;

public class MouseController : MonoBehaviour{

    // CONSTANTS

    private bool LEFTMOUSEBUTTON_DOWN;
    private bool RIGHTMOUSEBUTTON_DOWN;

    //===================================================================//

    private void Awake() {
        LEFTMOUSEBUTTON_DOWN  = Input.GetMouseButtonDown(0);
        RIGHTMOUSEBUTTON_DOWN = Input.GetMouseButtonDown(1);
    }

    public delegate void LeftClick(GameObject obj);
    public event LeftClick leftClick = delegate {};

    public delegate void RightClick(GameObject obj);
    public event RightClick rightClick = delegate {};

    private Camera mainCamera;

    private void Start(){
        mainCamera = Camera.main; // this is a GetObjectByTag() call, should be cached for performance reasons
    }

    private void Update(){
        if(LEFTMOUSEBUTTON_DOWN) leftClick(GetClickedObject());
        if(RIGHTMOUSEBUTTON_DOWN) rightClick(GetClickedObject());
    }

    private GameObject GetClickedObject(){
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        GameObject obj = hit.collider?.gameObject;
        return obj;
    }
}