using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue {

    private List<Event> queue;

    public EventQueue() {
        queue = new List<Event>();
    }

    public void Add(Event e) {
        queue.Add(e);

        int cur = queue.Count - 1;

        while(cur > 0) {
            int par = (cur - 1) / 2;
            if (queue[cur].y < queue[par].y)
                break;
            else {
                Event tmp = queue[cur];
                queue[cur] = queue[par];
                queue[par] = tmp;
            }
            cur = par;
        }
    }

    public Event Remove() {

        int lastItem = queue.Count - 1;
        if (lastItem < 0)
            return null;

        Event e = queue[0];
        queue[0] = queue[lastItem];
        queue.RemoveAt(lastItem);
        lastItem--;

        int par = 0;
        while (true) {
            int cur = par * 2 + 1;

            if (cur > lastItem)
                break;

            int r = cur + 1;

            if (r <= lastItem && queue[r].y > queue[cur].y)
                cur = r;

            if (queue[par].y < queue[cur].y)
                break;

            Event tmp = queue[cur];
            queue[cur] = queue[par];
            queue[par] = tmp;

            par = cur;
        }

        return e;
    }

    public bool isEmpty() {
        return queue.Count == 0;
    }

}
