using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreen : Singleton<TransitionScreen>
{
    public GameObject screenObject;

    public void SetVisible(bool visible)
    {
        screenObject.SetActive(visible);
    }
}
