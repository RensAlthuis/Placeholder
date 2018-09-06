public interface ISelectable {
    void Select(); // when clicking on a unit/tile
    void Deselect(); // when clicking on another unit/tile
    string Name(); // the name of the object

}