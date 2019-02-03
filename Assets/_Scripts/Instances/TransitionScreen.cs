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

    public void Transition(int worldType)
    {
        SetVisible(false);
        WorldManager.Instance.Transition((WorldType)worldType);
    }

    public void CameraPreview()
    {

    }
}
