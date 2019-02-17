using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TransitionButton : MonoBehaviour, IPointerEnterHandler
{
    public Vector3 targetRot, targetPivotRot;
    public float transitionTime = 0.2f;
    private Button button;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!button)
            button = GetComponent<Button>();
        if (!button.interactable)
            return;

        GameCamera camera = GameManager.Instance.gameCamera;
        camera.StartAnimate(Quaternion.Euler(targetRot), Quaternion.Euler(targetPivotRot), transitionTime);
    }
}
