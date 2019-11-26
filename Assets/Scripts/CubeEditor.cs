using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // View changes in Edit Mode
[SelectionBase]
[RequireComponent(typeof(WayPoint))]
public class CubeEditor : MonoBehaviour {

    WayPoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<WayPoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize, 
            0f, 
            waypoint.GetGridPos().y * gridSize
        );
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>(); // Get text from the top of cube
        string labelText = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y; 
        textMesh.text = labelText; // view position of cube on top
        gameObject.name = labelText;
    }
}
