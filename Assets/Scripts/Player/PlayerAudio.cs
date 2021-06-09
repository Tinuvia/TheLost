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
    public AudioClip[] hardSteps;
    public AudioClip[] mudSteps;
    public AudioClip[] bloodSounds;

    private AudioSource audioS;
    private Rigidbody2D rb;
    private float playerSpeed;
    private bool isFootstepsPlaying = false;
    private AudioClip[] stepClipToPlay;

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
        if (collision.CompareTag("BloodZone"))
        {
            int r = Random.Range(0, bloodSounds.Length);
            audioS.PlayOneShot(bloodSounds[r]);
            audioS.pitch = Random.Range(0.9f, 1.1f);
            audioS.volume = Random.Range(0.8f, 1.0f);
            Debug.Log("Playing Bloodsounds");
        }

        if (collision.CompareTag("MudFloor"))
        {
            stepClipToPlay = mudSteps;
            isFootstepsPlaying = false;
            PlayFootsteps(playerSpeed);
        }

        if (collision.CompareTag("Enemy")) {
            bool enemyIsDead = collision.GetComponent<Enemy>().isDead;
            if (!enemyIsDead)
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

        if (collision.CompareTag("MudFloor"))
        {
            stepClipToPlay = forestWalking; // reset to default sound
            isFootstepsPlaying = false;
            PlayFootsteps(playerSpeed);
        }        
    }


    private void PlayFootsteps(float playerSpeed)
    {
        if (stepClipToPlay == null)
        {
            stepClipToPlay = forestWalking;
        }

        if ((playerSpeed > 0.1) && !isFootstepsPlaying)
        {
            int r = Random.Range(0, stepClipToPlay.Length);
            audioS.clip = stepClipToPlay[r];
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
}
