using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiateMarker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject markerPrefab;
    [SerializeField]
    private Transform floorTransform;
    [SerializeField]
    private RectTransform canvasTransform;
    [SerializeField]
    private float floorWidth = 50;
    [SerializeField]
    private float floorLength = 50;
    [SerializeField]
    private Transform playerTransform;

    private RectTransform rectTransform;
    private float xMin, xMax, yMin, yMax, width, length;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 mousePos = eventData.position;
        PlaceMarker(mousePos);
    }

    private void PlaceMarker(Vector2 mousePos)
    {
        Vector3 currentScale = canvasTransform.localScale;
        SetScaledCoordinates(currentScale);
        float xCoord = (((mousePos.x - xMin) / width) * floorWidth) + floorTransform.position.x;
        float zCoord = (((mousePos.y - yMin) / length) * floorLength) + floorTransform.position.z;
        Vector3 markerPos = new Vector3(xCoord, floorTransform.position.y, zCoord);
        GameObject temp = Instantiate(markerPrefab);
        temp.transform.SetPositionAndRotation(markerPos, temp.transform.rotation);
        LookAtTarget tempLook = temp.GetComponentInChildren<LookAtTarget>();
        tempLook.target = playerTransform;
    }

    private void SetScaledCoordinates(Vector3 scale)
    {
        xMin = rectTransform.position.x + (rectTransform.rect.min.x * scale.x);
        xMax = rectTransform.position.x + (rectTransform.rect.max.x * scale.x);
        yMin = rectTransform.position.y + (rectTransform.rect.min.y * scale.y);
        yMax = rectTransform.position.y + (rectTransform.rect.max.y * scale.y);
        width = xMax - xMin;
        length = yMax - yMin;
    }
}
