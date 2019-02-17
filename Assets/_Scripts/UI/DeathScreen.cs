using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : Singleton<DeathScreen>
{
    public Image background;
    public float animationTime = 0.1f, frameTime = 0.02f, waitTime = 0.5f, targetAlpha = 0.7f;
    private bool animating;
    private const string bumpSFXName = "Bump";

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        ResetValues();
    }
    
    public void Show()
    {
        if (animating)
            return;
        
        Time.timeScale = 0;
        animating = true;
        AudioManager.Instance.PlaySFX(bumpSFXName);
        StartCoroutine(_Animate());
    }

    private IEnumerator _Animate()
    {
        background.gameObject.SetActive(true);

        var t = 0.0f;
        while (t < animationTime)
        {
            t += frameTime;
            background.SetAlpha(Mathf.Lerp(0, targetAlpha, t / animationTime));
            yield return new WaitForSecondsRealtime(frameTime);
        }
        WorldManager.currentWorldType = WorldType.World3D;
        LoadingScreen.Instance.ReloadScene();

        yield return new WaitForSecondsRealtime(waitTime);
        ResetValues();
    }

    private void ResetValues()
    {
        background.gameObject.SetActive(false);
        background.SetAlpha(0);
        Time.timeScale = 1.0f;
        animating = false;
    }
}
