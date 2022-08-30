using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Heap<T> where T: IHeapItem<T>
{
    T[] items;
    int currentItemCount;
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortHeapUp(item);
        currentItemCount++;
    }
    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortHeapDown(items[0]);
        return firstItem;
    }
   
    void SortHeapDown(T item)
    {
        while (true)
        {
            int leftChildIndex = item.HeapIndex * 2 + 1;
            int rightClildIndex = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (leftChildIndex < currentItemCount)
            {
                swapIndex = leftChildIndex;
                if (rightClildIndex < currentItemCount)
                {
                    swapIndex = items[leftChildIndex].CompareTo(items[rightClildIndex]) < 0 ? rightClildIndex:leftChildIndex ;
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    SwapItems(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else { return; }
        }
    }
    void SortHeapUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                SwapItems(item, parentItem);
            }
            else { break; }

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void SwapItems(T itemChild, T itemParent)
    {
        items[itemChild.HeapIndex] = itemParent;
        items[itemParent.HeapIndex] = itemChild;
        int tmpIndex = itemChild.HeapIndex;
        itemChild.HeapIndex = itemParent.HeapIndex;
        itemParent.HeapIndex = tmpIndex;
    }

    public void UpdateItem(T item)
    {
        SortHeapUp(item);
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }
    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }
}

public interface IHeapItem<T> : IComparable<T> 
{
    int HeapIndex
    {
        get;
        set;
    }
}
