using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public static int id = -1;

    [SerializeField]
    private string targetScene;
    [SerializeField]
    private int targetId;
    [SerializeField]
    private Vector3 spawnPosLocal;

    [Header("Lock Dialogue")]
    public string[] lines;
    public bool reloadScene;

    private void Start()
	{
		// CheckSpawn

        if(lines.Length > 0)
        {
            GetComponent<Collider>().isTrigger = false;
        }
	}

    public void CheckSpawn()
    {
        if (id == targetId)
        {
            id = -1;

            if (GameManager.Instance.IsIdle)
            {
                // Reset manager rotation;
                GameManager.Instance.transform.localRotation = Quaternion.identity;

                // Set player pos & rot
                var player = GameManager.Instance.player;
                var playerPos = transform.position + spawnPosLocal;
                player.transform.position = playerPos;
                player.transform.LookAt(playerPos + spawnPosLocal);

                // Set camera pos & rot
                var gameCamera = GameManager.Instance.gameCamera;
                gameCamera.transform.position = playerPos;
                gameCamera.SetLook(gameCamera.GetLookRot(), player.transform.rotation.eulerAngles);
            }
        }
    }

    public void SetLock(bool locked)
    {
        GetComponent<Collider>().isTrigger = !locked;
    }

    public void SpawnPlayer()
    {
        GameManager.Instance.player.transform.position = transform.position + spawnPosLocal;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue * 0.6f;
        Gizmos.DrawSphere(transform.position + spawnPosLocal, 1f);
    }

    private void OnTriggerStay(Collider col)
    {
        if (LoadingScreen.Instance.IsLoading)
            return;

        var player = col.GetComponentInParent<Player>();

        if(player && !col.isTrigger)
        {
            id = targetId;
            TutorialManager.Instance.HideTutorial();
            LoadingScreen.Instance.LoadScene(targetScene);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LoadingScreen.Instance.IsLoading)
            return;

        var col = collision.collider;
        var player = col.GetComponentInParent<Player>();

        if (player && !col.isTrigger)
        {
            GameManager.Instance.dialogueWindow.SetDialogue(lines, reloadScene);
        }
    }
}