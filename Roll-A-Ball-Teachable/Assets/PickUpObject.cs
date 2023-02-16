using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Material highlightMaterial;
    public Material defaultMaterial;

    public GameObject particleEffect;

    private bool isHighlighted = false;
    private Renderer rend;

    public float waitTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        Highlight();
    }

    public void Highlight()
    {
        if (!isHighlighted)
        {
            rend.material = highlightMaterial;
            isHighlighted = true;

            particleEffect.SetActive(false);
        }
    }

    IEnumerator WaitAndDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }

    public void UnHighlight()
    {
        if (isHighlighted)
        {
            rend.material = defaultMaterial;
            isHighlighted = false;
            particleEffect.SetActive(true);

            // wait for a second before disabling the object
            StartCoroutine(WaitAndDisable(waitTime));

        }
    }

}
