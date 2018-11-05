using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    public static WorldManager Instance { get; private set; }

    private static WorldType currentWorldType = WorldType.World3D;
    private World[] worlds;
    private World currentWorld;
    private WorldObject[] worldObjects;
    private InputPair transitionInput;

	void Awake ()
    {
        Instance = this;

        // Load worlds
        worlds = GetComponentsInChildren<World>(true);
        for (int i = 0; i < worlds.Length; i++)
        {
            if (worlds[i].worldType == currentWorldType)
            {
                currentWorld = worlds[i];
            }
            worlds[i].InitWorld();
            worlds[i].gameObject.SetActive(worlds[i].worldType == currentWorldType);
        }

        // Init world objects
        worldObjects = FindObjectsOfType<WorldObject>();
        for (int i = 0; i < worldObjects.Length; i++)
        {
            worldObjects[i].InitClone();
        }
    }

    private void Start()
    {
        // Load transition input
        transitionInput = InputHandler.Instance.GetInput(InputAction.CameraTransition);
    }

    private void Update()
    {
        if (currentWorld.GameCamera.Animating)
            return;

        if(transitionInput.GetAxisDown)
        {
            currentWorldType = currentWorldType == WorldType.World3D ? WorldType.WorldRight2D : WorldType.World3D;
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
        currentWorld.GameCamera.onTransitionComplete.AddListener(CheckPlayerCollision);
        currentWorld.GameCamera.TransitionOut();
    }

    private void CheckPlayerCollision()
    {
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
