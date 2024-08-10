using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeEffect : MonoBehaviour, IDragHandler
{
    public void Start()
    {
        Debug.Log("hi startign");
    }
    public void OnDrag(PointerEventData eventData) { 
        transform.localPosition= new Vector2(transform.localPosition.x, transform.localPosition.y + eventData.delta.y);
        Debug.Log("hi");
    }

    
}
