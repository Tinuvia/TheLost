using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoryTrigger : MonoBehaviour, IPointerClickHandler
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
                StorySystem.Show(content, transform.position);
                Debug.Log("Story: " + content);
                stringToShow++;
                StartCoroutine("Delay");

            }
            else
                StorySystem.Hide();
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
        StorySystem.Hide();
        Debug.Log("Hiding tooltip");
    }
}