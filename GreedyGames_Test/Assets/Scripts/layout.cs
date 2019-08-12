using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Linq;

public class layout : MonoBehaviour
{
    public Sprite[] texture;              // texture to apply on the images(top, bottom left, bottom right )
    public GameObject[] panels;           // all image panels on which texture is to be applied
    bool flag = false;                    // flag for fade im fade out animation
    List<RaycastResult> hitObjects = new List<RaycastResult>();     // to store all gameobjects after raycast
    float rotation = 60f;                              // for bottom right panel image for animation
    List<int> count = new List<int>() { 0, 1, 2, 3 }; // to randomise the selection of texture to be applied on the image panel
    float flicker = 8f;                               //to animate flicker effect

    // Start is called before the first frame update
    void Start()
    {
        int number = Random.Range(0, 3);

        panels[0].GetComponent<Image>().sprite = texture[count[number]];
        count.RemoveAt(number);
        number = Random.Range(0, 2);
        panels[1].GetComponent<Image>().sprite = texture[count[number]];
        count.RemoveAt(number);
        number = Random.Range(0, 1);
        panels[2].GetComponent<Image>().sprite = texture[count[number]];
        count.RemoveAt(number);
        panels[3].GetComponent<Image>().sprite = texture[count[0]];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject gameObject = GetobjectUndermouse();
            if (gameObject.tag == "TopHalfPlane")
                GetFadeInFadeOutAnimation(GetobjectUndermouse(), true);

            else if (gameObject.tag == "BottomRightPlane")
                GetChangeOrientationAnimation(gameObject, true);


        }
        else if (Input.GetMouseButtonUp(0))
        {
            GameObject gameObject = GetobjectUndermouse();
            if (gameObject.tag == "TopHalfPlane")
                GetFadeInFadeOutAnimation(gameObject, false);
        }
        if (Input.GetMouseButton(0))
        {
            GameObject gameObject = GetobjectUndermouse();
            if (gameObject.tag == "BottomLeftPlane")
                GetFlickerAnimation(gameObject);
        }
    }

    public GameObject GetobjectUndermouse()
    {
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0)
            return null;
        return hitObjects.First().gameObject;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        GameObject clickedObject = GetobjectUndermouse();
        if (clickedObject != null && clickedObject.tag == "Draggable")
        {
            return clickedObject.transform;

        }
        return null;
    }

    private void GetFadeInFadeOutAnimation(GameObject gameObject, bool flag)
    {
        if (flag)
        {
            Image objectToDragImage = gameObject.GetComponent<Image>();
            objectToDragImage.color = new Color(255f, 255f, 255f, 0.5f);
        }
        else
        {
            Image objectToDragImage = gameObject.GetComponent<Image>();
            objectToDragImage.color = new Color(255f, 255f, 255f, 1f);
        }
    }
    private void GetChangeOrientationAnimation(GameObject gameObject, bool flag)
    {
        gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, rotation));
        rotation += 60f;
    }
    private void GetFlickerAnimation(GameObject gameObject)
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + flicker);
        flicker = flicker > 0 ? -8f : 8f;
    }
}
