using UnityEngine;

public class CameraControl: MonoBehaviour
{

    private bool dragging;
    private Vector3 origin;

    public const float MINZOOM = 20.0f;
    public const float MAXZOOM = 60.0f;
    public const float LEFTBORDER = 0;
    public const float RIGHTBORDER = 1000;
    public const float BOTTOMBORDER = 0;
    public const float TOPBORDER = 500;

    void Start(){

    }

    void Update(){

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float magnitude = (mouseRay.origin.y / mouseRay.direction.y);
        Vector3 hit = mouseRay.origin -( mouseRay.direction * magnitude);

        if(Input.GetMouseButtonDown(2)){
            dragging = true;
            origin = hit;
        }else if(Input.GetMouseButtonUp(2)){
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
            origin = mouseRay.origin -( mouseRay.direction * magnitude);
            hit = origin;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0){
            Vector3 scrollDir = hit - Camera.main.transform.position;
            if((Camera.main.transform.position.y > MINZOOM && scroll > 0) ||
               (Camera.main.transform.position.y < MAXZOOM && scroll < 0)  ){
                Camera.main.transform.Translate(scrollDir * scroll, Space.World);
            }
        }

    }
}
