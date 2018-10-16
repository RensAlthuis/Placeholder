using UnityEngine;
using MapEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(TileData))]
[RequireComponent(typeof(MeshCollider))]
public class TileSelectable : MonoBehaviour, ISelectable
{

    private MeshRenderer meshRenderer;
    private TileData tileData;
    private bool selected;

    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
        tileData = GetComponent<TileData>();
    }

    public string Name()
    {
        return name;
    }

    public void Select()
    {
        selected = true;
        HighLight(true);
    }

    public void Deselect()
    {
        selected = false;
        HighLight(false);
    }

    private void HighLight(bool on){
        if(on){
            float H, S, V;
            Color.RGBToHSV(tileData.type.material.color, out H, out S, out V);
            meshRenderer.material.color = Color.HSVToRGB(H, S, 1);
        }else{
            meshRenderer.material = tileData.type.material;
        }
    }
}