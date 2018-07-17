public class BeachLineNode
{

    //This is probably better as a balance tree, like a red-black tree perhaps.

    public BeachLineNode parent;
    public bool isParabola;
    public string name;

    private BeachLineNode leftChild;
    public BeachLineNode LeftChild
    {
        get {
            return leftChild;
        }
        set {
            leftChild = value;
            leftChild.parent = this;
        }
    }

    private BeachLineNode rightChild;
    public BeachLineNode RightChild
    {
        get {
            return rightChild;
        }
        set {
            rightChild = value;
            rightChild.parent = this;
        }
    }


    //Must be instantiated as either edge or parabola
    protected BeachLineNode(string name)
    {
        this.name = name;
    }

    //step down left till we hit a leaf
    public Parabola LeftPar()
    {
        if (LeftChild == null)
        {
            return (Parabola)this;
        }
        return LeftChild.LeftPar();
    }

    //step down right till we hit a leaf
    public Parabola RightPar()
    {
        if (RightChild == null)
        {
            return (Parabola)this;
        }
        return RightChild.RightPar();
    }

}
