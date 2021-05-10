using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    // public AudioClip splashSound;
    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot auxInSnapshot;
    public AudioMixerSnapshot ambIdleSnapshot;
    public AudioMixerSnapshot ambInSnapshot;
    public float exitTransitionTime;
    public float enterTransitionTime;
    public float footstepsPitch;
    public AudioClip[] forestWalking;
    //public AudioClip[] hardSteps;
    //public AudioClip[] mudSteps;

    private AudioSource audioS;
    private Rigidbody2D rb;
    private float playerSpeed;
    private bool isFootstepsPlaying = false;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerSpeed = rb.velocity.magnitude;
        PlayFootsteps(playerSpeed);
    }

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


    private void PlayFootsteps(float playerSpeed)
    {            
        if((playerSpeed > 0.1) && !isFootstepsPlaying)
        {
            int r = Random.Range(0, forestWalking.Length);
            audioS.clip = forestWalking[r];
            audioS.pitch = footstepsPitch;
            audioS.Play();
            isFootstepsPlaying = true;
        }

        if ((playerSpeed < 0.1) && isFootstepsPlaying)
        {
            audioS.Stop();
            isFootstepsPlaying = false;
        }

    }
    // check if footsteps are playing
    // (check if right footstep type is playing)
    // if player is moving && !footstepsPlaying --> start playing footsteps (loop)
    // if player isn't moving && footstepsplaying --> stop playing

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
