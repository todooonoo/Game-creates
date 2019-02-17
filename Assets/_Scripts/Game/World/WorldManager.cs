using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    public static WorldManager Instance { get; private set; }

    public static WorldType currentWorldType = WorldType.World3D;
    private World[] worlds;
    private WorldObject[] worldObjects;
    private InputPair transitionInput3D, transitionInputRight, transitionInputUp, transitionInputFront;

    [HideInInspector]
    public World currentWorld;

    void Awake ()
    {
        Instance = this;
    }

    private void Start()
    {
        // Check spawn
        LevelTrigger[] triggers = FindObjectsOfType<LevelTrigger>();
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].CheckSpawn();
        }

        // Load worlds
        worlds = FindObjectsOfType<World>();
        for (int i = 0; i < worlds.Length; i++)
        {
            if (worlds[i].worldType == currentWorldType)
            {
                currentWorld = worlds[i];
            }
            if(worlds[i].worldType == WorldType.World3D)
            {
                worldObjects = worlds[i].GetComponentsInChildren<WorldObject>();
                for (int j = 0; j < worldObjects.Length; j++)
                {
                    worldObjects[j].InitClone();
                }
                var gameManager = worlds[i].GetComponentInChildren<GameManager>();
                gameManager.SetInstance();
            }
            worlds[i].gameObject.SetActive(worlds[i].worldType == currentWorldType);
            worlds[i].InitWorld();
        }
        
        // Load transition input
        transitionInput3D = InputHandler.Instance.GetInput(InputAction.Transition3D);
        transitionInputRight = InputHandler.Instance.GetInput(InputAction.TransitionRight);
        transitionInputUp = InputHandler.Instance.GetInput(InputAction.TransitionUp);
        transitionInputFront = InputHandler.Instance.GetInput(InputAction.TransitionFront);
        
    }

    private void Update()
    {
        if (currentWorld.GameCamera.Animating)
            return;

        CheckTransition();
    }

    private void CheckTransition()
    {
        var transition = false;
        if (transitionInput3D.GetAxisDown)
        {
            transition = currentWorldType != WorldType.World3D;
            currentWorldType = WorldType.World3D;
        } else if(transitionInputUp.GetAxisDown)
        {
            currentWorldType = currentWorldType == WorldType.WorldUp2D ? WorldType.World3D : WorldType.WorldUp2D;
            transition = true;
        } else if(transitionInputRight.GetAxisDown)
        {
            currentWorldType = currentWorldType == WorldType.WorldRight2D ? WorldType.World3D : WorldType.WorldRight2D;
            transition = true;
        } else if(transitionInputFront.GetAxisDown)
        {
            currentWorldType = currentWorldType == WorldType.WorldFront2D ? WorldType.World3D : WorldType.WorldFront2D;
            transition = true;
        }

        if(transition)
        {
            currentWorld.GameCamera.onTransitionComplete.AddListener(SwitchWorld);
            currentWorld.GameCamera.TransitionIn();
        }
    }
    
    public void Transition(WorldType worldType)
    {
        bool transition = false;

        if (worldType == WorldType.World3D)
        {
            transition = currentWorldType != WorldType.World3D;
            currentWorldType = WorldType.World3D;
        }
        else if (worldType == WorldType.WorldUp2D)
        {
            currentWorldType = currentWorldType == WorldType.WorldUp2D ? WorldType.World3D : WorldType.WorldUp2D;
            transition = true;
        }
        else if (worldType == WorldType.WorldRight2D)
        {
            currentWorldType = currentWorldType == WorldType.WorldRight2D ? WorldType.World3D : WorldType.WorldRight2D;
            transition = true;
        }
        else if (worldType == WorldType.WorldFront2D)
        {
            currentWorldType = currentWorldType == WorldType.WorldFront2D ? WorldType.World3D : WorldType.WorldFront2D;
            transition = true;
        }

        if (transition)
        {
            if(GameManager.Instance)
                GameManager.Instance.state = GameState.Event;
            currentWorld.GameCamera.onTransitionComplete.AddListener(SwitchWorld);
            currentWorld.GameCamera.TransitionIn();
        }
    }

    private void SwitchWorld()
    {
        currentWorld.gameObject.SetActive(false);
        currentWorld.TransitionOut();
        currentWorld = GetWorld(currentWorldType);
        currentWorld.SetPlayerCollision(false);
        currentWorld.gameObject.SetActive(true);
        currentWorld.TransitionIn();
        currentWorld.GameCamera.onTransitionComplete.AddListener(CheckPlayerCollision);
        currentWorld.GameCamera.TransitionOut();

    }

    private void CheckPlayerCollision()
    {
        currentWorld.SetPlayerCollision(true);
        currentWorld.CheckPlayerCollision();
        GameManager.Instance.state = GameState.Idle;
        GameManager.Instance.player.playerState = PlayerState.Idle;
    }

    public World GetWorld(WorldType worldType)
    {
        for(int i = 0; i < worlds.Length; i++)
        {
            if (worlds[i].worldType == worldType)
                return worlds[i];
        }
        return null;
    }

    public GameCamera GameCamera
    {
        get { return currentWorld.GameCamera; }
    }
}
