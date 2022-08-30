using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Barrack : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]private BarrackBuilding _barrack;
    [SerializeField] private GameObject unit;
    public bool isCreated;
 
    public void CreatedBuilding()
    {
       float min_X = (transform.localScale.x / 2) + 4;
        Vector3 spawnPos = transform.position.x < min_X ? transform.GetChild(2).position : transform.GetChild(1).position;
        _barrack = new BarrackBuilding(transform.localScale.x, transform.localScale.y, spawnPos,unit);

        isCreated = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isCreated)
        {
            CanvasControl.Instance.OpenInformation();
            CanvasControl.Instance.Select(unit, _barrack.spawnPos);
        }

    }
   
   
}
