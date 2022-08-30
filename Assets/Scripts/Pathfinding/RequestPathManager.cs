using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RequestPathManager : MonoBehaviour
{
    public static RequestPathManager Instance { get; private set; }

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentRequest;
    PathFinder pathFinder;
    bool isProcessingPath;
    private void Awake()
    {
        Instance = this;
        pathFinder = GetComponent<PathFinder>();
    }
    public static void RequestPath(GameObject unit,GameObject target, Action<Vector3[], bool> callBack)
    {
        
        PathRequest newRequest = new PathRequest(unit,target,callBack);
        Instance.pathRequestQueue.Enqueue(newRequest);
        Instance.TryProcessNext();

    }
    void TryProcessNext()
    {
        if (!isProcessingPath&&pathRequestQueue.Count>0)
        {
            currentRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathFinder.StartFindPath(currentRequest.unit, currentRequest.target);
        }
    }

    public void FinishedProcessingPath(Vector3[] path,bool success)
    {
        currentRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }
    struct PathRequest
    {
        public GameObject unit;
        public GameObject target;
        public Action<Vector3[], bool> callback;
        public PathRequest(GameObject _unit,GameObject _target, Action<Vector3[], bool> _callback)
        {
            unit = _unit;
            target = _target;
            callback = _callback;
        }
    }
}
