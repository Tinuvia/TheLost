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
    public AudioMixerSnapshot ambIdleSnapshot;
    public AudioMixerSnapshot ambInSnapshot;
    public float exitTransitionTime;
    public float enterTransitionTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("Water"))
        {
            audioS.PlayOneShot(splashSound);
        }
        */
        if (collision.CompareTag("Enemy")) {
            auxInSnapshot.TransitionTo(enterTransitionTime);
        }
        if (collision.CompareTag("Ambience"))
        {
            ambInSnapshot.TransitionTo(enterTransitionTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("Water"))
        {
            audioS.PlayOneShot(splashSound);
        }
        */
        if (collision.CompareTag("Enemy")) {
            idleSnapshot.TransitionTo(exitTransitionTime);
        }
        if (collision.CompareTag("Ambience"))
        {
            ambIdleSnapshot.TransitionTo(enterTransitionTime);
        }
    }
}
