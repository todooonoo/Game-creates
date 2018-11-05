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
        Instance = this;
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
