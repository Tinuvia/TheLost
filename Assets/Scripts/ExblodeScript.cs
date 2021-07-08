using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExblodeScript : MonoBehaviour
{
    public float fieldofImpact;
    public float force;
    public LayerMask LayerToHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            explode();
        }  
    }

    void explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, LayerToHit);
    }
}
