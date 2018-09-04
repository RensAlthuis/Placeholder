using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour{
    [Header("UI Settings")]
    public GameObject selectedPanel;
    public Text selectedName;
    public Image selectedImage;

    private UIview UIview;

    private void Start(){
        UIview = new UIview(selectedPanel, selectedName, selectedImage);
    }
}