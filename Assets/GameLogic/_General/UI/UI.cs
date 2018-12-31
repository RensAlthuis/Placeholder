using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour{
    [Header("UI Settings")]
    public GameObject selectedPanel;
    public Text selectedName;
    public Image selectedImage;

    private void Start(){
        SelectableController.UpdateSelected += UpdatePanel;
    }

    private void UpdatePanel(ISelectable selected){
        if (selected == null){
            selectedPanel.SetActive(false);
        }else{
            selectedPanel.SetActive(true);
            selectedName.text = selected.Name();
        }

    }
}