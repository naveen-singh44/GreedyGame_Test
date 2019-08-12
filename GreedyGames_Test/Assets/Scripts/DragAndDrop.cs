using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 topHalfPlanePos;          // to store the current positon of top half plane
    Vector2 bottomLeftPlanePos;       // to store the current positon of bottom left plane
    Vector2 bottomRightPlanePos;      // to store the current positon of bottom right plane
    Vector2 objectDragged;            // to store the current obejct position 
    public GameObject topHalfPlane;    // top half plane refrence 
    public GameObject bottomLeftPlane; // bottom left plane reference
    public GameObject bottomRightPlane;// bottom right plane reference
    private void Update()
    {
        topHalfPlanePos = topHalfPlane.GetComponent<RectTransform>().position;
        bottomLeftPlanePos = bottomLeftPlane.GetComponent<RectTransform>().position;
        bottomRightPlanePos = bottomRightPlane.GetComponent<RectTransform>().position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        objectDragged = this.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        GetComponent<Image>().raycastTarget = false;
    }

    // to drag and drop images 
    public void OnEndDrag(PointerEventData eventData)
    {
        layout layout = new layout();
        GameObject gameObject = layout.GetobjectUndermouse();
        if (gameObject.tag == "BottomRightPlane")
        {
            this.transform.position = bottomRightPlanePos;
            gameObject.transform.position = objectDragged;
        }
        else if (gameObject.tag == "BottomLeftPlane")
        {
            this.transform.position = bottomLeftPlanePos;
            gameObject.transform.position = objectDragged;
        }
        else if (gameObject.tag == "TopHalfPlane")
        {
            // its not working as required
            this.transform.position = topHalfPlanePos;
            Vector2 pos = new Vector2(300f, 110f);
            gameObject.transform.position =pos;
        }
        GetComponent<Image>().raycastTarget = true;
    }
}
