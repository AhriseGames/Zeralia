using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<(T Item, int Priority)> elements = new List<(T, int)>();

    // Add an item to the queue
    public void Enqueue(T item, int priority)
    {
        elements.Add((item, priority));
        elements.Sort((x, y) => x.Priority.CompareTo(y.Priority)); // Sort by priority (lowest first)
    }

    // Remove and return the highest-priority item
    public T Dequeue()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        T bestItem = elements[0].Item; // Get the element with the highest priority
        elements.RemoveAt(0); // Remove it from the queue
        return bestItem;
    }

    public bool TryPeek(out T item, out int priority)
    {
        if (elements.Count == 0)
        {
            item = default;
            priority = default;
            return false;
        }

        item = elements[0].Item;
        priority = elements[0].Priority;
        return true;
    }


    // Check if the queue is empty
    public bool IsEmpty()
    {
        return elements.Count == 0;
    }
    public void Clear()
    {
        elements.Clear(); // Removes all combatants
    }

    //  Get queue count
    public int Count()
    {
        return elements.Count;
    }

    // Peek: See next item without removing it
    public T Peek()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        return elements[0].Item;
    }

    // TryDequeue: Safe Dequeue (Returns false if empty, instead of throwing an error)
    public bool TryDequeue(out T item, out int priority)
    {
        if (elements.Count == 0)
        {
            item = default;
            priority = default;
            return false;
        }

        item = elements[0].Item;
        priority = elements[0].Priority;
        elements.RemoveAt(0);
        return true;
    }

    // Get all elements (for debugging, doesn't remove anything)
    public List<(T Item, int Priority)> GetAllElements()
    {
        return new List<(T, int)>(elements); // Returns a copy of the queue
    }
}