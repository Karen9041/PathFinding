using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// A sorted linked list
/// </summary>
public class SortedLinkedList<T> : LinkedList<T> where T:IComparable
{
    #region Constructor

    public SortedLinkedList() : base()
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds the given item to the list
    /// </summary>
    /// <param name="item">item to add to list</param>
    public void Add(T item)
    {
        // add your code here
        LinkedListNode<T> node = new LinkedListNode<T>(item);
        if(First == null)
        {
            AddFirst(node);
        }
        else
        {
            LinkedListNode<T> current = First;
            while(current != null && current.Value.CompareTo(item) < 0)
            {
                current = current.Next;
            }
            if(current == null)
            {
                AddLast(node);
            }
            else
            {
                AddBefore(current, node);
            }
        }

    }

    /// <summary>
    /// Repositions the given item in the list by moving it
    /// forward in the list until it's in the correct
    /// position. This is necessary to keep the list sorted
    /// when the value of the item changes
    /// </summary>
    public void Reposition(T item)
    {
        // add your code here
        LinkedListNode<T> node = Find(item);
        if(node == null || node.Previous == null)
        {
            return;
        }

        T value = node.Value;
        Remove(node);
        Add(value);
    }
    #endregion
}
