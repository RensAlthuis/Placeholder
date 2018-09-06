using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class SelectableController : MonoBehaviour{ // All things that can be selected

    public static ISelectable current;

    public delegate void onSelect(ISelectable obj);
    public static event onSelect Click = delegate {};

    public static void doClick(ISelectable obj){
            current?.Deselect();
            current = obj;
            Click(obj);
    }

}