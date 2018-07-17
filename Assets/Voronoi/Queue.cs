using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Queue
{
    //Works like a simple binary heap
    List<Event> queue = new List<Event>();

    public Boolean IsEmpty()
    {
        return queue.Count == 0;
    }

    public void Enqueue(Event e)
    {
        //add element to bottom of heap
        queue.Add(e);

        //compare added element with parent, if they are in correct order stop
        //if not swap element with parent and return to previous step

        int current = queue.Count - 1;
        int parent = (current - 1) / 2;

        while (current > 0)
        {
            if (queue[current] < queue[parent])
            {
                //in order, so break out of loop
                break;
            }

            //not in order, switch places and update indices
            Event t = queue[current];
            queue[current] = queue[parent];
            queue[parent] = t;
            current = parent;
            parent = (current - 1) / 2;
        }
    }

    private Event RemoveAt(int i)
    {
        //replace element in heap with last element on last level

        Event e = queue[i];
        queue[i] = queue[queue.Count - 1];
        queue.RemoveAt(queue.Count - 1);

        //compare the new root with its children; if they are in the correct order, stop.
        //if not, swap the element with on of its children and return to the previous step. 
        //(Swap with its smaller child in a min-heap and its larger child in a max-heap.)

        int current = i;
        int left = 2 * current + 1;
        int right = 2 * current + 2;

        while (true)
        {

            if (left <= queue.Count - 1 && queue[left] > queue[current])
            {
                current = left;
            }
            if (right <= queue.Count - 1 && queue[right] > queue[current])
            {
                current = right;
            }

            if (current == i)
            {
                break;
            }

            Event t = queue[i];
            queue[i] = queue[current];
            queue[current] = t;

            i = current;
            left = 2 * current + 1;
            right = 2 * current + 2;
        }

        return e;
    }

    public Event Dequeue()
    {

        return RemoveAt(0);
    }

    public void Delete(Event e)
    {
        if (e == null) return;

        int i = queue.IndexOf(e);
        if (i == -1)
        {
            return;
        }

        RemoveAt(i);
    }
}