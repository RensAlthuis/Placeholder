using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola {
    /*
		isLeaf		: flag whether the node is Leaf or internal node
		site		: pointer to the focus point of parabola (when it is parabola)
		edge		: pointer to the edge (when it is an edge)
		cEvent		: pointer to the event, when the arch disappears (circle event)
		parent		: pointer to the parent node in tree
	*/

    public bool isLeaf;
    public Point site;
    public Edge edge;
    public Event cEvent;
    public Parabola parent;

    /*
		Constructors of the class (empty for edge, with focus parameter for an arch).
	*/

    public Parabola() {
        //site = null;
        cEvent = null;
        edge = null;
        parent = null;
        isLeaf = false;
    }

    public Parabola(Point s) {
        site = s;
        edge = null;
        cEvent = null;
        parent = null;
        isLeaf = true;
    }

    /*
		Access to the children (in tree).
	*/

    public void SetLeft(Parabola p) { _left = p; p.parent = this; }
    public void SetRight(Parabola p) { _right = p; p.parent = this; }

    public Parabola Left() { return _left; }
    public Parabola Right() { return _right; }

    /*
		Some useful tree operations

		GetLeft			: returns the closest left leave of the tree
		GetRight		: returns the closest right leafe of the tree
		GetLeftParent	: returns the closest parent which is on the left
		GetLeftParent	: returns the closest parent which is on the right
		GetLeftChild	: returns the closest leave which is on the left of current node
		GetRightChild	: returns the closest leave which is on the right of current node
	*/

    public Parabola GetLeft() {
        return GetLeftParent().GetLeftChild();
    }


    public Parabola GetRight() {
        return GetRightParent().GetRightChild();
    }

    public Parabola GetLeftParent() {
        Parabola par = parent;
        Parabola pLast = this;
        while (par.Left() == pLast) {
            if (par.parent == null) { 
                return null;
            }
            pLast = par;
            par = par.parent;
        }
        return par;
    }

    public Parabola GetRightParent() {
        Parabola par = parent;
        Parabola pLast = this;
        while (par.Right() == pLast) {
            if (par.parent == null) {
                return null;
            }
            pLast = par;
            par = par.parent;
        }
        return par;
    }

    public Parabola GetLeftChild() {

        Parabola par = Left();
        while (!par.isLeaf) par = par.Right();
        return par;
    }

    public Parabola GetRightChild() {

        Parabola par = Right();
        while (!par.isLeaf) par = par.Left();
        return par;

    }

    private Parabola _left;
    private Parabola _right;
}
