using UnityEngine;
using System.Collections;

public class PewPew : MonoBehaviour
{
    [Header("Bullet Variables")]
    public float bulletSpeed;
    public float fireRate, bulletDamage;
    public bool isAuto;

    [Header("Initial Setup")]
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;

    public int maxAmmo = 26;
    private int currentAmmo;
    public float realoadTime = 1f;
    private bool isReloading = false;  

    private float timer;

    public Animator animator;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    private void Update()
    {
        if (isReloading)
            return;


        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        if(timer > 0)
        {
            timer -= Time.deltaTime / fireRate;
        }

        if (isAuto)
        {
            if(Input.GetButton("Fire1")&& timer <= 0)
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetButtonDown("Fire1")&& timer <= 0)
            {
                Shoot();
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true; 
        Debug.Log("Reloading..");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(realoadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("WorldObjectHolder").transform);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnTransform.forward * bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<Bullet>().damage = bulletDamage;
        bullet.transform.rotation = bulletSpawnTransform.rotation;

        timer = 1;

    }
}
