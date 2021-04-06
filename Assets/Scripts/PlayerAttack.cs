using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    public Transform firePoint;
    public float fireForce;
    public int ammo;
    public Animator animator;
    public TextMeshProUGUI ammoText;

    void Start()
    {
        ammoText.text = ammo.ToString();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Fire();
        } else if (Input.GetMouseButtonUp(0)) {
            StopFiring();
        }
    }

    public void Fire() {
        if (ammo > 0) {
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                pooledProjectile.transform.position = firePoint.position;
            }
            pooledProjectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            UpdateAmmo(-1);
            StartCoroutine("SetFalse", pooledProjectile);
            animator.SetBool("IsFiring", true);
        }
    }

    public void StopFiring() {
        animator.SetBool("IsFiring", false);
    }

    public void UpdateAmmo(int mod)
    {
        ammo += mod;
        ammoText.text = ammo.ToString();
    }

    IEnumerator SetFalse(GameObject objectToDisable) {

        yield return new WaitForSeconds(3.0f); // disable projectile after 1.0f secs
        objectToDisable.SetActive(false);
    }

}
