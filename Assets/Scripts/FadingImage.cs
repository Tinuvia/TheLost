using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingImage : MonoBehaviour
{
    public void FadeInImage(Image img, float time)
    {
        StartCoroutine(FadeIn(img, time));
    }

    public void FadeOutImage(Image img, float time)
    {
        StartCoroutine(FadeOut(img, time));
    }

    public void FadeInAndOutImage(Image img, float time)
    {
        StartCoroutine(FadeInAndOut(img, time));
    }


    public void FadeOutAndInImage(Image img, float time)
    {
        StartCoroutine(FadeOutAndIn(img, time));
    }



    // fade from transparent to opaque
    IEnumerator FadeIn(Image img, float time)
    {

        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime/time)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }

    }

    // fade from opaque to transparent
    IEnumerator FadeOut(Image img, float time)
    {
        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime / time)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    IEnumerator FadeInAndOut(Image img, float time)
    {
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime / time)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }

        //Temp to Fade Out
        yield return new WaitForSeconds(1);

        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime / time)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    IEnumerator FadeOutAndIn(Image img, float time)
    {
        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime / time)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }

        //Temp to Fade In
        yield return new WaitForSeconds(1);

        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime / time)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}
