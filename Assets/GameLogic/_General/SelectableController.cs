using UnityEngine;
using UnityEditor;

public class SelectableController : MonoBehaviour { // All things that can be selected

    public static ISelectable current;

    public delegate void onSelect(ISelectable obj);
    public static event onSelect UpdateSelected = delegate { };

    private void Awake() {
        MouseController m = FindObjectOfType<MouseController>();
        m.leftClick += doClick;
    }

    private static void doClick(GameObject obj) {
        ISelectable s = obj.GetComponent<ISelectable>();
        current?.Deselect();
        current = s;
        s?.Select();
        UpdateSelected(s);
    }
}