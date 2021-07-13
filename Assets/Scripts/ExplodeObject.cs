using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeObject : MonoBehaviour
{
    [SerializeField] string poolParticlesTag;
    private Animator anim;

    public void StartExplosion()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(true);
        Debug.Log("Starting explosion ...");
        anim.SetTrigger("Explode");
        ObjectPooler.Instance.SpawnFromPool(poolParticlesTag, transform.position, Quaternion.identity);
    }
}
