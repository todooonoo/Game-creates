using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InteractType
{
    Push = 0,
    Move = 1,
    Talk = 2,
}

[System.Serializable]
public struct InteractIconStruct
{
    public InteractType interactType;
    public Sprite iconSprite;
    public Vector2 size;
    public string text;
}

public class InteractIcon : MonoBehaviour
{
    public Image mainIcon;
    public Text mainText;
    public InteractIconStruct[] structs;
    
    public void SetIcon(InteractType type)
    {
        for(int i = 0; i < structs.Length; i++)
        {
            if(structs[i].interactType == type)
            {
                mainIcon.sprite = structs[i].iconSprite;
                mainIcon.rectTransform.sizeDelta = structs[i].size;
                mainText.text = structs[i].text;
                break;
            }
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
