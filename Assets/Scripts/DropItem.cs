using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    private int itemID;
    private int returnID;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject itemDroped = DragDrop.itemBeingDragged;
        int itemID = itemDroped.GetComponent<DragDrop>().itemID;

        Destroy(DragDrop.itemBeingDragged);
        DragDrop.itemBeingDragged = null;

        SetItemID(itemID);
    }

    public void SetItemID(int itemID)
    {
        returnID = itemID;
    }

    public int GetItemID()
    {
        return returnID;
    }
}
