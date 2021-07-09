using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image darkBackground;
    public Image lightBackground;
    public Image palmTrees;
    public GameObject titleMenu;
    public float timeDarkBackground = 10f;
    public float timeMenu = 11.5f;
    public float timeLightBackground = 10f;

    FadingImage fadingImageScript;

    // play start intro:
    // fade in DarkBackground 
    // set Backgrund Light deactive
    // set title active
    // set options active

    private void Start()
    {
        fadingImageScript = GetComponent<FadingImage>();
        StartCoroutine("PlayIntro");
    }

    private void playImage(Image img, float time)
    {
        fadingImageScript.FadeInImage(img, time);
    }


    IEnumerator PlayIntro()
    {
        playImage(darkBackground, timeDarkBackground);
        fadingImageScript.FadeOutImage(lightBackground, timeDarkBackground);
        yield return new WaitForSeconds(timeMenu);
        //playImage(palmTrees, timePalmTrees);
        //yield return new WaitForSeconds(timePalmTrees);
        titleMenu.SetActive(true);
        //yield return new WaitForSeconds(timeLightBackground);
        lightBackground.gameObject.SetActive(false);
    }
}
