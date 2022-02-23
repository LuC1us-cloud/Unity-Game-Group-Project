using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float dampingSpeed = 0.05f;
    private Vector3 velocity = Vector3.zero;
    private RectTransform draggingObject;
    private Vector3 draggingObjectOriginalPosition;
    private Transform draggingObjectOriginalParent;

    readonly string[] slots = { "Feet slot", "Legs slot", "Chest slot", "Head slot", "RightHand slot", "LeftHand slot" };
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObject, eventData.position, eventData.pressEventCamera, out Vector3 worldPoint))
        {
            draggingObject.position = worldPoint;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        draggingObject = GetComponent<RectTransform>();
        draggingObjectOriginalPosition = draggingObject.position;
        draggingObjectOriginalParent = draggingObject.parent;
        // get gameObject with the tag "InventoryUI"
        GameObject inventoryUI = GameObject.FindGameObjectWithTag("Equipment Parent");
        draggingObject.SetParent(inventoryUI.transform);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // if the GameObject directly under the cursor is named "Slot" set draggingObject to it's child and update it's position
        // if (eventData.pointerCurrentRaycast.gameObject.name == "Feet slot")
        // {
        //     draggingObject = eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        //     draggingObject.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        // }
        // bool isSlot = false;
        // foreach (string slot in slots)
        // {
        //     if (eventData.pointerEnter.name == slot)
        //     {
        //         isSlot = true;
        //         break;
        //     }
        // }
        // if (isSlot)
        // {
        //     draggingObject.SetParent(eventData.pointerEnter.transform);
        //     return;
        // }
        // else
        // {
        //     draggingObject.SetParent(draggingObjectOriginalParent);
        //     draggingObject.position = draggingObjectOriginalPosition;
        // }
    }
    private void Awake()
    {
        draggingObject = transform as RectTransform;
    }
}
