using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SizeUpdate : MonoBehaviour
{
    public GameObject canavasScript;              // to get the width and height of the canavas
    private Vector2 initialCanavasSize;          //to store the initial size of the canvas

    public float decreaseArea = 0.2f;            // decrease in the width and height of the canavas

    public GameObject text;                      // to display the current screen size 
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        initialCanavasSize = canavasScript.GetComponent<RectTransform>().localScale;

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void GetDecreaseSizeFunction()
    {
        Vector2 currentParameter = canavasScript.GetComponent<RectTransform>().localScale;
        if (currentParameter.x > decreaseArea + 0.1)
        {
            canavasScript.GetComponent<RectTransform>().localScale = new Vector2(currentParameter.x - decreaseArea, currentParameter.y - decreaseArea);
            text.GetComponent<TextMeshProUGUI>().text = GetAreaOfCanavas().ToString();
        }
        else
        {
            canavasScript.GetComponent<RectTransform>().localScale = new Vector2(0f, 0f);
            text.GetComponent<TextMeshProUGUI>().text = "0 % Visible";
        }

    }
    public void GetIncreaseSizeFunction()
    {
        Vector2 currentParameter = canavasScript.GetComponent<RectTransform>().localScale;
        if (currentParameter.x < 1)
        {
            canavasScript.GetComponent<RectTransform>().localScale = new Vector2(currentParameter.x + decreaseArea, currentParameter.y + decreaseArea);
            text.GetComponent<TextMeshProUGUI>().text = GetAreaOfCanavas();
        }
        else
        {
            text.GetComponent<TextMeshProUGUI>().text = "100 % Visible";
        }
    }

    private string GetAreaOfCanavas()
    {
        float currentWidth = canavasScript.GetComponent<RectTransform>().localScale.x;
        float currentHeight = canavasScript.GetComponent<RectTransform>().localScale.y;

        float originalHeight = initialCanavasSize.y;
        float originalWidth = initialCanavasSize.x;

        float percentageOfOriginalArea = (1 - (((originalHeight * originalWidth) - (currentHeight * currentWidth)) / (originalHeight * originalWidth))) * 100;

        return percentageOfOriginalArea.ToString() + " % Visible";
    }


}
