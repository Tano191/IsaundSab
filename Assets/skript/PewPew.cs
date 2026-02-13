using System.Collections;
using UnityEngine;

public class PewPew : MonoBehaviour
{
    [Header("Bullet Variables")]
    public float bulletSpeed;
    public float fireRate, bulletDamage;
    public bool isAuto;

    [Header("Initial Setup")]
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;

    [Header("Aiming / Zoom")]
    public Camera playerCamera;
    public float normalFOV = 77f;
    public float aimFOV = 40f;
    public float zoomSpeed = 10f;

    public int maxAmmo = 26;
    private int currentAmmo;
    public float realoadTime = 1f;
    private bool isReloading = false;

    private float timer;
    [Header("For the Fancy")]
    public Animator animator;
    public ParticleSystem shooteffect;
    // audio
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;



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

        if (timer > 0)
        {
            timer -= Time.deltaTime / fireRate;
        }

        if (isAuto)
        {
            if (Input.GetButton("Fire1") && timer <= 0)
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && timer <= 0)
            {
                Shoot();
            }
        }

        HandleZoom();
    }

    void HandleZoom()
    {
        float targetFOV = normalFOV;

        if (Input.GetButton("Fire2"))
        {
            targetFOV = aimFOV;
        }

        playerCamera.fieldOfView = Mathf.Lerp(
            playerCamera.fieldOfView,
            targetFOV,
            Time.deltaTime * zoomSpeed
        );
    }

    IEnumerator Reload()
    {
        audioSource.PlayOneShot(reloadSound);
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

        // pew
        if (shootSound != null)
            audioSource.PlayOneShot(shootSound);
            shooteffect.Play();

        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawnTransform.position,
            Quaternion.identity,
            GameObject.FindGameObjectWithTag("WorldObjectHolder").transform
        );

        bullet.GetComponent<Rigidbody>().AddForce(
            bulletSpawnTransform.forward * bulletSpeed,
            ForceMode.Impulse
        );

        bullet.GetComponent<Bullet>().damage = bulletDamage;
        bullet.transform.rotation = bulletSpawnTransform.rotation;

        timer = 1;
    }
}
