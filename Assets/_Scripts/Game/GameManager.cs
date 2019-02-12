using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle,
    Event,
}

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public Player player;
    
    public GameCamera gameCamera;
    public Camera Camera { get { return gameCamera.Camera; } }

    public GameState state = GameState.Idle;
    public bool IsIdle { get { return state == GameState.Idle; } }
    
    public InteractIcon interactIcon;
    public DialogueWindow dialogueWindow;
    public TransitionScreen transitionScreen;

    [Header("Save States")]
    public bool transitionUnlocked;
    public bool upTransitionUnlocked;

    private void Start()
    {
        if(dialogueWindow)
            dialogueWindow.InitUI();
    }

    private void OnEnable()
    {
        if (!Instance)
        {
            Instance = this;
        }
        LockCursor(true);
    }

    private void OnDisable()
    {
        if(Instance == this)
            Instance = null;
    }

    private void Update()
    {
        if (gameCamera.Animating || !IsIdle)
            return;

        player.HandleUpdate();
        gameCamera.HandleUpdate();
    }

    private void FixedUpdate()
    {
        if (gameCamera.Animating || !IsIdle)
            return;

        player.HandleFixedUpdate();
    }
    
    public void UnlockCursor()
    {
        Debug.Log("Unlock");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor(bool forceLock)
    {
        Cursor.lockState = forceLock ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !forceLock;
    }

    public void ShowInteractIcon(Vector3 worldPos)
    {
        interactIcon.transform.position = Camera.WorldToScreenPoint(worldPos);
        interactIcon.SetActive(true);
    }

    public void HideInteractIcon()
    {
        if(interactIcon)
            interactIcon.SetActive(false);
    }

    public void SetInteractIcon(InteractType type)
    {
        if (interactIcon)
            interactIcon.SetIcon(type);
    }
}
