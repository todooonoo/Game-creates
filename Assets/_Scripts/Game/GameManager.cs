using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private Player player;

    // Camera
    [SerializeField]
    private GameCamera gameCamera;
    public Camera Camera { get { return gameCamera.Camera; } }
    
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
        if (gameCamera.Animating)
            return;

        player.HandleUpdate();
        gameCamera.HandleUpdate();
    }

    private void FixedUpdate()
    {
        if (gameCamera.Animating)
            return;

        player.HandleFixedUpdate();
    }
}
