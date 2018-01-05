using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragAndDropInventoryScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Vector3 startPosition;
    public GameObject dragObject;
    public GameObject player;
	public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position= Input.mousePosition;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        string objectPointer = null;
        if (eventData.pointerCurrentRaycast.gameObject!= null)
        {
            objectPointer = eventData.pointerCurrentRaycast.gameObject.name;
        }
        if (objectPointer != null)
        {
            if (!(objectPointer.StartsWith("slot")) && objectPointer != "Inventory")
            {
                DestroyObject();
            }
        }
        else
        {
            DestroyObject();
        }
        
            
        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    public void DestroyObject() {
        int instance = int.Parse(gameObject.transform.parent.name.Substring(4));
        player = gameObject.transform.parent.gameObject.GetComponent<TooltipInventoryScript>().player;
        player.GetComponent<InventoryController>().DestroyObject(instance);

    }
}
