using UnityEngine;

public class CameraControl: MonoBehaviour
{

    public float zoomspeed;
    public float scrollSpeed;
    private bool dragging;
    private Vector3 origin;

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
            Camera.main.transform.Translate(diff, Space.World);
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            magnitude = (mouseRay.origin.y / mouseRay.direction.y);
            origin = mouseRay.origin -( mouseRay.direction * magnitude);
        }
    }
}
