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

    // Camera
    public GameCamera gameCamera;
    public Camera Camera { get { return gameCamera.Camera; } }

    public GameState state = GameState.Idle;
    public bool IsIdle { get { return state == GameState.Idle; } }

    private void OnEnable()
    {
        if(!Instance)
            Instance = this;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor(bool forceLock)
    {
        Cursor.lockState = forceLock ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !forceLock;
    }

}
