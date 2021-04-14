using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    // public AudioClip splashSound;
    public AudioSource audioS;
    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot auxInSnapshot;
    public float exitTransitionTime;
    public float enterTransitionTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player entered trigger");
        /*
        if (collision.CompareTag("Water"))
        {
            audioS.PlayOneShot(splashSound);
        }
        */
        if (collision.CompareTag("EnemyZone"))
        {
            auxInSnapshot.TransitionTo(enterTransitionTime);
            Debug.Log("Player entered enemy zone");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Player exited trigger");
        /*
        if (collision.CompareTag("Water"))
        {
            audioS.PlayOneShot(splashSound);
        }
        */
        if (collision.CompareTag("EnemyZone"))
        {
            idleSnapshot.TransitionTo(exitTransitionTime);
            Debug.Log("Player left enemy zone");
        }
    }
}
