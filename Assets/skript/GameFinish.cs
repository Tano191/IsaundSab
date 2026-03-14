using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameFinish : MonoBehaviour
{
    [Header("References")]
    public CollectCounter count;
    public Renderer portalRenderer;
    public AudioSource portalSound;

    private Material portalMaterial;
    private GameObject portalObj;
    private bool portalActive = false;
    private Collider portalCollider;

    void Start()
        //portal script combined with my existing game finish portal
    {
        portalObj = portalRenderer.gameObject;
        portalMaterial = portalRenderer.material;
        portalCollider = GetComponent<Collider>();

      
        portalObj.SetActive(false);
        portalMaterial.SetFloat("_Alpha", 0);
        portalCollider.enabled = false; 
    }

    void Update()
    {
       
        if (!portalActive && count.GetCount() >= Collectible.total)
        {
            ActivatePortal();
        }
    }

    void ActivatePortal()
    {
        portalActive = true;
        StartCoroutine(PortalActivation());
    }

    private IEnumerator PortalActivation()
    {
        portalObj.SetActive(true);
        portalSound.Play();

        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            portalMaterial.SetFloat("_Alpha", 1f - timer * 0.75f);
            yield return null;
        }

        portalMaterial.SetFloat("_Alpha", 0f);
        portalCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && portalActive)
        {
            Debug.Log("Portal Entered - Loading Scene");
            SceneManager.LoadScene("Level Finished");
        }
    }
}