using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmo : MonoBehaviour
{
    [SerializeField] private int ammoCount;
    private PlayerAttack playerAttackScript;
    public AudioClip reloadSound;
    private Animator _anim;

    void Start()
    {
        _anim = GameObject.Find("Player").GetComponent<Animator>();
        playerAttackScript = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(reloadSound, transform.position);
            playerAttackScript.UpdateAmmo(ammoCount);
            _anim.SetTrigger("Reload");
            // When OBJECT POOLING - setactive(false)
            Destroy(gameObject);
        }
    }
}
