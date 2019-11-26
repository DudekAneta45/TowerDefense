using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

    public bool isExplored = false; // access from another class
    public WayPoint exploredFrom;
    public bool isPlaceable = true;

    const int gridSize = 10; // can't change value


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize), // x = 6 : x/10 = 0.6, RoundToInt = 1
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    /*
    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }
    */

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this); // add tower at this particular waypoint
            }
            
        }
        
    }
}
