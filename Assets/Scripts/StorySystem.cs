using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySystem : MonoBehaviour
{
    private static StorySystem current;
    public Story story;

    private void Awake()
    {
        // add failsafe to ensure only one singleton exists? carries over levels?
        current = this;
    }

    public static void Show(string content, Vector2 storyObjectPosition)
    {
        current.story.SetText(content, storyObjectPosition);
        current.story.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.story.gameObject.SetActive(false);
    }
}
