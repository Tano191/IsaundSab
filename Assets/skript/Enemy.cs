using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{
    public float health;
    public Animator animator;

    public AudioSource audioSource;
    public AudioClip deathSound;

    private bool isDead = false;

    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            animator.SetBool("IsDead", true);
            StartCoroutine(DeathRoutine());
        }
    }

    IEnumerator DeathRoutine()
    {
        audioSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(1.2f); 
        Destroy(gameObject);
    }
}
