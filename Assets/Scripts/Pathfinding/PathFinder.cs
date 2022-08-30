using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinder : MonoBehaviour
{
    RequestPathManager requestManager;
    public static PathFinder Instance { get; private set; }
   
    
    private void Awake()
    {
        Instance = this;
        requestManager = GetComponent<RequestPathManager>();
    }
    public void StartFindPath(GameObject character,GameObject target)
    {
 
        StartCoroutine(FindPath(character, target));
    }
    IEnumerator FindPath(GameObject character, GameObject target)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        Node startNode = character.GetComponent<Tile>().node;
        Node targetNode = target.GetComponent<Tile>().node;

        
        Heap<Node> openSet = new Heap<Node>(WorldCreator.Instance.MaxSize);
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closeSet.Add(currentNode);

            if(currentNode==targetNode)
            {
                pathSuccess = true;
                break;
            }
            
            foreach(Node neighbour in GetNeighbours(currentNode.obj))
            {
                if(!neighbour.walkable || closeSet.Contains(neighbour))
                {
                    continue;
                }

                int movementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if(movementCostToNeighbour<neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = movementCostToNeighbour;
                    neighbour.hCost = GetDistance(currentNode, neighbour);
                    neighbour.prnt = currentNode;

                    if (!openSet.Contains(neighbour)) { openSet.Add(neighbour); }
                }

            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = ReTracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] ReTracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
       Node current = end;
        while(current != start)
        {
            path.Add(current);
            current = current.prnt;
        }
        
       Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        waypoints.Add(path[0].pos);
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY- path[i].gridY);
           if(directionNew != directionOld)
            {
                waypoints.Add(path[i].pos);
            }
           
        }
    
        return waypoints.ToArray();
    }
    int GetDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);
        if (distX > distY) { return 14 * distY + 10 * (distX - distY); }

        return 14 * distX + 10 * (distY - distX);

    }
  
   
    List<Node> GetNeighbours(GameObject obj)
    {
       
        List<Node> neighbours = new List<Node>();
        foreach (GameObject n in obj.GetComponent<Tile>().neighbours)
        {
            neighbours.Add(n.GetComponent<Tile>().node);
        }
        return neighbours;
    }
}
