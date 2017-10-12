using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	The class for storing place / circle event in event queue.

	point		: the point at which current event occurs (top circle point for circle event, focus point for place event)
	pe			: whether it is a place event or not
	y			: y coordinate of "point", events are sorted by this "y"
	arch		: if "pe", it is an arch above which the event occurs
*/
public class Event{

    public Point point;
    public bool isPlaceEvent;
    public float y;
    public Parabola arch;

    public Event(Point p, bool _isPlaceEvent) {
        point = p;
        isPlaceEvent = _isPlaceEvent;
        y = p.y;
        arch = null;
    }
}
