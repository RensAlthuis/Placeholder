using UnityEngine;
using MapEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(TileData))]
public class TileSelectable : Selectable
{

    private MeshRenderer meshRenderer;
    TileData tileData;

    protected override void Awake(){
        base.Awake();
        meshRenderer = GetComponent<MeshRenderer>();
        tileData = GetComponent<TileData>();
    }

    public override string Name()
    {
        return name;
    }

    public override void Select()
    {
        selected = true;
        HighLight(true);
    }

    public override void Deselect()
    {
        selected = false;
        HighLight(false);
    }

    private void HighLight(bool on){
        if(on){
            float H, S, V;
            Color.RGBToHSV(TerrainLoader.GetMaterial(tileData.type).color, out H, out S, out V);
            GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(H, S, 1);
        }else{
            GetComponent<MeshRenderer>().material = TerrainLoader.GetMaterial(tileData.type);
        }
    }
}