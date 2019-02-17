using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpRobot2D : MonoBehaviour
{
    private WorldObjectClone clone;
    private RobotFromTo robotScript;
    private bool updateDone;

    private void Start()
    {
        clone = GetComponentInParent<WorldObjectClone>();
        robotScript = clone.origin.GetComponent<RobotFromTo>();
    }

    private void Update()
    {
        HandleUpdate();
    }

    private void LateUpdate()
    {
        updateDone = false;
    }

    public void HandleUpdate()
    {
        if (updateDone)
            return;
        robotScript.HandleUpdate();
        clone.SetCurrentPosition(WorldManager.Instance.currentWorld);
        updateDone = true;
    }
}
