using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public abstract class Selectable : MonoBehaviour{ // All things that can be selected

    MeshCollider meshCollider;
    protected bool selected;

    public delegate void onSelect(Selectable obj);
    public static event onSelect Click;

    protected virtual void Awake(){
        meshCollider = GetComponent<MeshCollider>();
    }

    public void SetCollisionMesh(Mesh mesh){
        meshCollider.sharedMesh = mesh;
    }

    void OnMouseDown(){
        if (Click != null){
            Click(this);
        }
    }

    // Clicking
    public abstract void Select(); // when clicking on a unit/tile
    public abstract void Deselect(); // when clicking on another unit/tile
    public abstract string Name(); // the name of the object

    // picture?
    // info?
}