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
    public TextMeshProUGUI ammoText;
    private Animator animator;
    private int isFiringHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        ammoText.text = ammo.ToString();
        isFiringHash = Animator.StringToHash("IsFiring");
    }

    private void Update() {
        if (!UIManager.isPaused) // checking to make sure we aren't in the pause menu
        {
            if(Input.GetMouseButtonDown(0)) {
                Fire();
            }
            if (Input.GetMouseButtonUp(0)) {
                StopFiring();
            }
        }

    }

    public void Fire() {
        if (ammo > 0) {     
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                pooledProjectile.transform.position = firePoint.position;
                pooledProjectile.transform.rotation = firePoint.rotation;
                Debug.Log("Projectile fired");
            }
         
            // GameObject pooledProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            pooledProjectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            UpdateAmmo(-1);
            StartCoroutine("SetFalse", pooledProjectile);
            animator.SetBool(isFiringHash, true);
        }
    }

    public void StopFiring() {
        animator.SetBool(isFiringHash, false);
    }

    public void UpdateAmmo(int mod)
    {
        ammo += mod;
        ammoText.text = ammo.ToString();
        Debug.Log("Ammo count " + ammo);
    }

    IEnumerator SetFalse(GameObject objectToDisable) {

        yield return new WaitForSeconds(3.0f); // disable projectile after 1.0f secs
        if(objectToDisable.activeSelf)  {
            objectToDisable.SetActive(false);
            Debug.Log("Projectile disabled from time");
        }
    }

}
