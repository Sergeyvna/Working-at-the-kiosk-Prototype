using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    static public GameObject itemBeingDragged;
    public int itemID;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = Instantiate(gameObject);

        RectTransform tmpRT = gameObject.GetComponent<RectTransform>();
        RectTransform rt = itemBeingDragged.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(tmpRT.sizeDelta.x, tmpRT.sizeDelta.y);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        Transform canvas = GameObject.FindGameObjectWithTag("UI Canvas").transform;
        itemBeingDragged.transform.SetParent(canvas);
        itemBeingDragged.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
       itemBeingDragged.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(DragDrop.itemBeingDragged);
        DragDrop.itemBeingDragged = null;
    }
}
