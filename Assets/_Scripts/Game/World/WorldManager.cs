using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    public static WorldManager Instance { get; private set; }

    private static WorldType currentWorldType = WorldType.World3D;
    private World[] worlds;
    private World currentWorld;
    private WorldObject[] worldObjects;
    private InputPair transitionInput3D, transitionInputRight, transitionInputUp, transitionInputFront;

	void Awake ()
    {
        Instance = this;
    }

    private void Start()
    {

        // Load worlds
        worlds = FindObjectsOfType<World>();
        for (int i = 0; i < worlds.Length; i++)
        {
            if (worlds[i].worldType == currentWorldType)
            {
                currentWorld = worlds[i];
            }
            worlds[i].gameObject.SetActive(worlds[i].worldType == currentWorldType);
            worlds[i].InitWorld();
        }

        // Init world objects
        worldObjects = FindObjectsOfType<WorldObject>();
        for (int i = 0; i < worldObjects.Length; i++)
        {
            worldObjects[i].InitClone();
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

    private void SwitchWorld()
    {
        currentWorld.gameObject.SetActive(false);
        currentWorld.TransitionOut();
        currentWorld = GetWorld(currentWorldType);
        currentWorld.gameObject.SetActive(true);
        currentWorld.TransitionIn();
        currentWorld.SetPlayerCollision(false);
        currentWorld.GameCamera.onTransitionComplete.AddListener(CheckPlayerCollision);
        currentWorld.GameCamera.TransitionOut();
    }

    private void CheckPlayerCollision()
    {
        currentWorld.SetPlayerCollision(true);
        currentWorld.CheckPlayerCollision();
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
