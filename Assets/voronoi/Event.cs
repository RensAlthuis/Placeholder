using System;
using System.Collections.Generic;
using UnityEngine;

public class Event
{

    public Point point;
    public float y;
    public bool isPointEvent;
    public Parabola par;

    public Event(float y, Point v, bool isPointEvent)
    {
        this.y = y;
        this.point = v;
        this.isPointEvent = isPointEvent;
    }

    public static bool operator <(Event a, Event b)
    {
        if (a == null || b == null)
        {
            throw new ArgumentNullException();
        }

        if (Mathf.Approximately(a.y, b.y))
        {
            if (a.point.x < b.point.x && !Mathf.Approximately(a.point.x, b.point.x))
            {
                return true;
            }
            return false;
        } else if (a.y < b.y)
        {
            return true;
        }
        return false;
    }

    public static bool operator >(Event a, Event b)
    {
        return !(a < b);
    }

    override public string ToString()
    {
        return point.ToString();
    }

}
