using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    public Tooltip tooltip;

    private void Awake()
    {
        // add failsafe to ensure only one singleton exists? carries over levels?
        current = this;
    }

    public static void Show(string content)
    {
        current.tooltip.SetText(content);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
