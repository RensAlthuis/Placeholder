
public interface Selectable { // All things that can be selected
    
    // Clicking
    void Select(); // when clicking on a unit/tile
    bool Deselect(Selectable newSelection); // when clicking on another unit/tile
    void Highlight(); // when a unit/tile can be selected (like neighbouring tiles)

    // UI
    string Name(); // the name of the object
    // picture?
    // info?
}