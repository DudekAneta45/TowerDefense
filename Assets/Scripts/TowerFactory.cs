using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    [SerializeField] Transform towerParentTransform;

    Queue<Tower> queueOfTowers = new Queue<Tower>();

    public void AddTower(WayPoint baseWaypoint)
    {
        int numberOfTowers = queueOfTowers.Count;

        if (numberOfTowers < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void InstantiateNewTower(WayPoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, new Vector3(baseWaypoint.transform.position.x, baseWaypoint.transform.position.y, baseWaypoint.transform.position.z + 4), Quaternion.identity);
        newTower.transform.parent = towerParentTransform;
        baseWaypoint.isPlaceable = false;

        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        queueOfTowers.Enqueue(newTower); //put new tower on the queue
    }

    private void MoveExistingTower(WayPoint newBaseWaypoint) 
    {
        var oldTower = queueOfTowers.Dequeue(); //take bottom tower off queue

        oldTower.baseWaypoint.isPlaceable = true; //free-up the block
        newBaseWaypoint.isPlaceable = false;

        oldTower.baseWaypoint = newBaseWaypoint;

        oldTower.transform.position = new Vector3(newBaseWaypoint.transform.position.x, newBaseWaypoint.transform.position.y, newBaseWaypoint.transform.position.z + 4);
        
        queueOfTowers.Enqueue(oldTower); // put the old tower on top of queue
    }
}
