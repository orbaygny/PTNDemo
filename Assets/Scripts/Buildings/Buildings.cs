using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buildings
{

    public float sizeX,sizeY;

    public Buildings(float sizeX,float sizeY)
    {
    
        this.sizeX = sizeX;
        this.sizeY = sizeY;
    }
}


public class BarrackBuilding : Buildings
{
   public Vector3 spawnPos;
    public GameObject unit;
    public BarrackBuilding(float sizeX,float sizeY, Vector3 spawnPos,GameObject unit):base(sizeX,sizeY)
    {
        this.spawnPos = spawnPos;
    }

   
}