using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Unit : MonoBehaviour, IPointerDownHandler
{
    public GameObject target;
    public GameObject currentTile;
    float speed =30f;
    Vector3[] path;
    int targetIndex;

    bool find;
    public bool isSelected;
    
   
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
       
        if (pathSuccessful)
        {
            
            path = newPath;   
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");

        }
    }
    private void OnMouseOver()
    {

      /*  if (Input.GetMouseButtonDown(0))
        {
           
            Select();
        */
       
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed*Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grid"))
        {
            currentTile = collision.gameObject;
        }
    }

    private void Select()
    {
        if(Selector.Instance.unit != null)
        {
            if (!GameObject.ReferenceEquals(Selector.Instance.unit,gameObject))
            {
                Selector.Instance.NewSelect(gameObject);
            }
            else if (GameObject.ReferenceEquals(Selector.Instance.unit, gameObject))
            {
                Selector.Instance.DeSelect();
            }
        }
        else
        {
            Selector.Instance.NewSelect(gameObject);
        }
     
    }
    public void RequestPathForUnit()
    {
        targetIndex = 0;
        RequestPathManager.RequestPath(currentTile, target, OnPathFound);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
            {
            Select();
        }
        
    }
}
