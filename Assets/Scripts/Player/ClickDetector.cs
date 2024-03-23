using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerPress;

        if (clickedObject != null)
        {
            Debug.Log("Clicked on UI element layer: " + clickedObject.layer);
            Debug.Log("Clicked on UI element name: " + clickedObject.name);
            Debug.Log("Clicked on UI element: active" + clickedObject.active);
            // Perform your action here
        }
    }
}
