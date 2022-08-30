using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject target;
    public GameObject currentTile;
    float speed =5f;
    Vector3[] path;
    int targetIndex;

    bool find;
    
    private void Start()
    {
        Debug.Log(currentTile);
        Debug.Log(target);
      
        RequestPathManager.RequestPath(currentTile, target, OnPathFound);
    }
   
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
       
        if (pathSuccessful)
        {
            path = newPath;
           // StopCoroutine("FollowPath");
           //StartCoroutine("FollowPath");

        }
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
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
                yield return null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grid"))
        {
            currentTile = collision.gameObject;
        }
    }
}
