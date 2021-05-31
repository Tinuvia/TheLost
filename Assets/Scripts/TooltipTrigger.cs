using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerClickHandler
{
    public string[] storyElements;
    public float timeToShow = 3f;

    private int stringToShow = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (stringToShow < storyElements.Length)
            {
                string content = storyElements[stringToShow];
                TooltipSystem.Show(content);
                Debug.Log("Story: " + content);
                stringToShow++;
                // StartCoroutine("Delay");
                
            } else
                TooltipSystem.Hide();
        }
    }

    /*
    public void OnMouseExit()
    {
        TooltipSystem.Hide();
    }
    */

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(timeToShow); // disable tooltip after x secs
        TooltipSystem.Hide();
        Debug.Log("Hiding tooltip");
    }
}
