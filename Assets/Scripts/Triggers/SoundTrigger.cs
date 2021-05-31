using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioClip[] triggerSoundClips;
    private AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioS.pitch = Random.Range(0.6f, 1.5f);
            audioS.volume = Random.Range(0.6f, 1.4f);
            int r = Random.Range(0, triggerSoundClips.Length);
            audioS.PlayOneShot(triggerSoundClips[r]);
        }
    }
}
