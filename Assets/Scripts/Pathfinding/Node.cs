using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node:IHeapItem<Node>
{
    public Vector3 pos;
    public bool walkable;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    int heapIndex;
    public Node prnt;

    public GameObject obj;
 

    public Node(Vector3 pos, bool walkable,int gridX,int gridY,int gCost,int hCost,GameObject obj)
    {
        this.pos = pos;
        this.walkable = walkable;
        this.gridX = gridX;
        this.gridY = gridY;
        this.gCost = gCost;
        this.hCost = hCost;
        this.obj = obj;   
    }


    public int FCost
    {
        get { return gCost + hCost; }
    }

    public int HeapIndex 
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = FCost.CompareTo(nodeToCompare.FCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }
}
