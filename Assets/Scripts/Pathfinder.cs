using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] WayPoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>();
    Queue<WayPoint> queue = new Queue<WayPoint>(); // define a queue
    [SerializeField] bool isRunning = true;
    WayPoint searchCenter;
    public List<WayPoint> path = new List<WayPoint>();

    [SerializeField] Material pathMaterial;

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<WayPoint> GetPath()
    {
        if(path.Count ==0) //calculate path only once 
        { 
            LoadBlocks();
            BreadFirstSearch();
            CreatePath();
        }
        return path;
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);

        WayPoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }
        SetAsPath(startWaypoint);
        path.Reverse();
        SetColor();
    }

    
    private void SetColor()
    {
        foreach(WayPoint waypoint in path)
        {
            var block =waypoint.transform.Find("Block_Friendly");
            block.GetComponent<MeshRenderer>().material = pathMaterial;
        }
    }
    

    private void SetAsPath(WayPoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void BreadFirstSearch()
    {
        queue.Enqueue(startWaypoint); //add to end of queue
        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue(); // return the front of queue
            HaltIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
    }

    private void HaltIfEndFound()
    {
        if(searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if(!isRunning)
        {
            return;
        }
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordianates = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordianates))
            {
                QueueNewNeighbours(neighbourCoordianates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordianates)
    {
        WayPoint neighbour = grid[neighbourCoordianates];
        if(neighbour.isExplored || queue.Contains(neighbour)) //do nothing if neighbour was already explored or he is in queue
        {

        }
        else
        { 
            queue.Enqueue(neighbour); //add to end of queue
            //print("Queueing " + neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<WayPoint>();
        foreach (WayPoint waypoint in waypoints)
        {
            //overlapping blocks?
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if(isOverlapping)
            {
                Debug.LogWarning("Skipping Overlapping block" + waypoint);
            }
            else
            { 
                grid.Add(waypoint.GetGridPos(), waypoint); //add to dictionary
            }
        }
            
    }


}
