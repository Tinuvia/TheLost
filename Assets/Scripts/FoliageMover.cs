using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FoliageMover : MonoBehaviour
{
    public AudioClip[] foliageRustle;
    private Animator anim;
    private AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("move");
            audioS.pitch = Random.Range(0.6f, 1.5f);
            audioS.volume = Random.Range(0.6f, 1.4f);
            int r = Random.Range(0, foliageRustle.Length);
            audioS.PlayOneShot(foliageRustle[r]);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.ResetTrigger("move");
        }
    }

}
