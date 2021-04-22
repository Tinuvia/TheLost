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
    public AudioClip[] forestSteps;
    public AudioClip[] hardSteps;
    public AudioClip[] mudSteps;

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

    /* Need to figure out Raycast in 2D first
    public void FootSteps()
    {
        RaycastHit2D hit;
        Ray2D ray = new Ray(transform.position, transform.back);
        int r = Random.Range(0, 3);
        if(Physics2D.Raycast(ray, out hit, 1f))
        {
            switch(hit.transform.tag)
            {
                case "MudFloor":
                    audioS.PlayOneShot(mudSteps[r]);
                    break;

                case "ForestFloor":
                    audioS.PlayOneShot(forestSteps[r]);
                    break;

                case "HardFloor":
                    audioS.PlayOneShot(hardSteps[r]);
                    break;

                default:
                    audioS.PlayOneShot(forestSteps[r]);
                    break;

            }
        }
    }
    */
}
