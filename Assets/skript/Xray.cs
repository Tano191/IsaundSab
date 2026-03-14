using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XRay : MonoBehaviour
{
    [Header("Settings")]
    public int itemsNeededForXRay = 10;

    [Header("Outline Settings")]
    public Color outlineColor = Color.cyan;
    public float outlineWidth = 5f;
    public float pulseSpeed = 2f;
    public float minWidth = 3f;
    public float maxWidth = 8f;

    private List<Outline> outlines = new List<Outline>();
    private bool xrayActive = false;

    void Update()
        //made with tutorial btw :3
    {
     
        CollectCounter counter = FindObjectOfType<CollectCounter>();

        if (counter != null && counter.GetCount() >= itemsNeededForXRay && !xrayActive)
        {
            ActivateXRay();
        }


        if (xrayActive)
        {
            PulseOutlines();
        }
    }

    void ActivateXRay()
    {
        xrayActive = true;
        Debug.Log("✓ X-Ray activated! Highlighting remaining collectibles.");

        Collectible[] collectibles = FindObjectsOfType<Collectible>(); //old goes brrr deal with it unity

        foreach (var collectible in collectibles)
        {
            if (collectible != null)
            {

                Outline outline = collectible.gameObject.GetComponent<Outline>();

                if (outline == null)
                {
                    outline = collectible.gameObject.AddComponent<Outline>();
                }

                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = outlineColor;
                outline.OutlineWidth = outlineWidth;

                outlines.Add(outline);
            }
        }
    }

    void PulseOutlines()
    {
        float pulse = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        float width = Mathf.Lerp(minWidth, maxWidth, pulse);


        outlines.RemoveAll(o => o == null);


        foreach (var outline in outlines)
        {
            if (outline != null)
            {
                outline.OutlineWidth = width;
            }
        }
    }
}