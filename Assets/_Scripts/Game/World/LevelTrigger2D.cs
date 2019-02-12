using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger2D : MonoBehaviour
{
    [SerializeField]
    private string targetScene;
    [SerializeField]
    private int targetId;
    [SerializeField]
    private WorldType worldType = WorldType.WorldRight2D;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (LoadingScreen.Instance.IsLoading)
            return;

        var player = col.GetComponentInParent<Player>();

        if (player && !col.isTrigger)
        {
            LevelTrigger.id = targetId;
            TutorialManager.Instance.HideTutorial();
            LoadingScreen.Instance.LoadScene(targetScene);
        }
    }
}
