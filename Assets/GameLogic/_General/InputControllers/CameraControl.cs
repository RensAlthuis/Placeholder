using UnityEngine;

public class CameraControl: MonoBehaviour {

    // CONSTANTS 
    
    // (+ = decrease, - = increase) These variables DO NOT depend on the size of the map!! Though they do depend on the height and angle of the camera
    [SerializeField] private float LEFTBORDER   = 0;   // decreasing the amount at which you can go sideways
    [SerializeField] private float BOTTOMBORDER = -10; // increasing the amount at which you can go down
    [SerializeField] private float RIGHTBORDER  = 200; // decreasing the amount at which you can go right
    [SerializeField] private float TOPBORDER    = 200; // decreasing the amount at which you can go up
    [SerializeField] private float MINZOOM      = 20.0f; 
    [SerializeField] private float MAXZOOM      = 40.0f;

    private bool DRAG_BUTTON_PRESSED;
    private bool DRAG_BUTTON_RELEASED;
    private float SCROLL;

    //===================================================================//

    private bool dragging;
    private Vector3 origin;

    //===================================================================//

    private void Awake() {
        DRAG_BUTTON_PRESSED  = Input.GetMouseButtonDown(2);
        DRAG_BUTTON_RELEASED = Input.GetMouseButtonUp(2);
        SCROLL = Input.GetAxis("Mouse ScrollWheel");
    }

    private void Start() {
        transform.position = new Vector3(LEFTBORDER, MAXZOOM, BOTTOMBORDER); // Start position
    }

    private void Update(){
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float magnitude = (mouseRay.origin.y / mouseRay.direction.y);
        Vector3 hit = mouseRay.origin -( mouseRay.direction * magnitude);

        if(DRAG_BUTTON_PRESSED){
            dragging = true;
            origin = hit;
        } else if(DRAG_BUTTON_RELEASED){
            dragging = false;
        }

        if(dragging){
            Vector3 diff = origin - hit;
            Vector3 pos = Camera.main.transform.position;
            if(pos.x < LEFTBORDER && diff.x < 0){ diff.x = 0; }
            if(pos.x > RIGHTBORDER && diff.x > 0){ diff.x = 0; }
            if(pos.z < BOTTOMBORDER && diff.z < 0){ diff.z = 0; }
            if(pos.z > TOPBORDER && diff.z > 0){ diff.z = 0; }
            Camera.main.transform.Translate(diff, Space.World);

            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            magnitude = (mouseRay.origin.y / mouseRay.direction.y);
            origin = mouseRay.origin - ( mouseRay.direction * magnitude);
            hit = origin;
        }

        float scroll = SCROLL;
        if (Mathf.Abs(scroll) > 0){
            Vector3 scrollDir = hit - Camera.main.transform.position;
            if((Camera.main.transform.position.y > MINZOOM && scroll > 0) ||
               (Camera.main.transform.position.y < MAXZOOM && scroll < 0)  ){
                Camera.main.transform.Translate(scrollDir * scroll, Space.World);
            }
        }
    }
}