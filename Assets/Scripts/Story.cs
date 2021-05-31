using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Story : MonoBehaviour
{
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;
    public int characterWrapLimit;
    public RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, Vector2 storyObjectPosition)
    {
        contentField.text = content;
        int contentLength = contentField.text.Length;
        layoutElement.enabled = (contentLength > characterWrapLimit) ? true : false; // if text is longer than limit, enable layout element
        transform.position = storyObjectPosition;
    }


    // if used in Update, the toolip sticks to the mouse
    // here, it sticks in the initial screen position (not world position)
    // goal --> to get the tooltip to stay at the transform of the object that the tip belongs to
    // or to get placed in the world coordinates of the mouseposition
    /*
    private void PlaceTooltip()
    {
        Vector2 mousePosition = Input.mousePosition;

        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = mousePosition;
    }
    */
}
