using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : Singleton<BlackScreen> {

    [SerializeField]
    private Image background;

    public void SetAlpha(float alpha)
    {
        background.SetAlpha(alpha);
        background.gameObject.SetActive(alpha > 0);
    }
}
