using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiateMarker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    [Tooltip("Prefab to instantiate when clicking the map")]
    private GameObject markerPrefab;
    [SerializeField]
    [Tooltip("Position of the floor plane")]
    private Transform floorTransform;
    [SerializeField]
    [Tooltip("Canvas transform used for scale")]
    private RectTransform canvasTransform;
    [SerializeField]
    [Tooltip("X dimension of the floor")]
    private float floorWidth = 50;
    [SerializeField]
    [Tooltip("Z dimension of the floor")]
    private float floorLength = 50;
    [SerializeField]
    [Tooltip("Player transform to orient markers towards")]
    private Transform playerTransform;

    // The rect transform of the attached component (the main map)
    private RectTransform rectTransform;
    // Constraints for map marker positions
    private float xMin, xMax, yMin, yMax, width, length;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Get the position of the mouse and place a marker
        // OnPointerClick will only trigger if this object is clicked
        Vector2 mousePos = eventData.position;
        PlaceMarker(mousePos);
    }

    /// <summary>
    /// Place a marker aligned with the pllace clicked on the map
    /// </summary>
    /// <param name="mousePos">The location of the mouse when clicked</param>
    private void PlaceMarker(Vector2 mousePos)
    {
        // Get the scale based on the current canvas scale
        Vector3 currentScale = canvasTransform.localScale;
        SetScaledCoordinates(currentScale);
        // Calculate corresponding world coordinates using scale, map constraints, and floor properties
        float xCoord = (((mousePos.x - xMin) / width) * floorWidth) + floorTransform.position.x;
        float zCoord = (((mousePos.y - yMin) / length) * floorLength) + floorTransform.position.z;
        // Set the position for a new marker using the above coordinates and the height of the floor
        Vector3 markerPos = new Vector3(xCoord, floorTransform.position.y, zCoord);
        // Instantiate a marker
        GameObject temp = Instantiate(markerPrefab);
        // Set its position
        temp.transform.SetPositionAndRotation(markerPos, temp.transform.rotation);
        // Get a reference to its LookAtTarget component and set the target to the player
        LookAtTarget tempLook = temp.GetComponentInChildren<LookAtTarget>();
        tempLook.target = playerTransform;
    }

    /// <summary>
    /// Use the canvas scale to adjust the map boundary values
    /// </summary>
    /// <param name="scale"></param>
    private void SetScaledCoordinates(Vector3 scale)
    {
        // The map position is anchored to the center, and boundaries are asessed accordingly
        xMin = rectTransform.position.x + (rectTransform.rect.min.x * scale.x);
        xMax = rectTransform.position.x + (rectTransform.rect.max.x * scale.x);
        yMin = rectTransform.position.y + (rectTransform.rect.min.y * scale.y);
        yMax = rectTransform.position.y + (rectTransform.rect.max.y * scale.y);
        width = xMax - xMin;
        length = yMax - yMin;
    }
}
