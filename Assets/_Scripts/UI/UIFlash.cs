using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlash : MonoBehaviour {

    [SerializeField] private float flashTime = 1;
    [SerializeField] private float flashSpeed = 1;
    [SerializeField] private float currentAlpha;
    private CanvasGroup canvasGroup;
    
	void Awake ()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (!canvasGroup)
        {
            Debug.Log("No CanvasGroup found: " + name);
            Destroy(this);
        }
        canvasGroup.alpha = currentAlpha;
    }
	
	void Update ()
    {
        currentAlpha += flashSpeed * Time.deltaTime / flashTime;

        if(currentAlpha >= 1.0f)
        {
            currentAlpha = 1.0f;
            flashSpeed *= -1;
        } else if(currentAlpha <= 0.0f)
        {
            currentAlpha = 0.0f;
            flashSpeed *= -1;
        }
        canvasGroup.alpha = currentAlpha;
    }
}
