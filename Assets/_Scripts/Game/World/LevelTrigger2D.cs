using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger2D : MonoBehaviour
{
    [SerializeField]
    private string targetScene;
    [SerializeField]
    private int targetId;

    [Header("Lock Dialogue")]
    public string[] lines;
    public bool reloadScene;

    private void Start()
    {
        if (lines.Length > 0)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LoadingScreen.Instance.IsLoading)
            return;

        var col = collision.collider;
        var player = col.GetComponentInParent<Player>();

        if (player && !col.isTrigger)
        {
            TransitionScreen.Instance.dialogueWindow.SetDialogue(lines, reloadScene);
        }
    }
}
