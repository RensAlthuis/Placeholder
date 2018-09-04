using UnityEngine;
using UnityEngine.UI;

public class UIview {

    private GameObject selectedPanel;
    private Text selectedName;
    private Image selectedImage;

    // Use this for initialization
    public UIview(GameObject selectedPanel, Text selectedName, Image selectedImage) {
        this.selectedPanel = selectedPanel;
        this.selectedName = selectedName;
        this.selectedImage = selectedImage;
        // Unselect();
	}

    // public void Selected(Selectable selected) {
    //     selectedPanel.SetActive(true);
    //     selectedName.text = selected.Name();
    // }

    // public void Unselect() {
    //     selectedPanel.SetActive(false);
    // }
}
