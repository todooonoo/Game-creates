using System;
using System.Collections.Generic;
using UnityEngine;

public enum InputAction
{
    Attack = 0,
    Pick = 1,
    Jump = 2,
    Dash = 3,
    Drag = 4,
    TransitionMain = 10,
    Transition3D = 99,
    TransitionRight = 100,
    TransitionUp = 101,
    TransitionFront = 102,
    Interact = 200,
}

[Serializable]
public abstract class InputPair
{
    public InputAction action;
    protected bool pressed, prevPressed;

    public abstract void HandleUpdate();

    public bool GetAxis { get { return pressed; } }
    public bool GetAxisDown { get { return !prevPressed && pressed; } }
    public bool GetAxisUp { get { return prevPressed && !pressed; } }
}

[Serializable]
public class KeyInputPair : InputPair {

    public KeyCode keyCode;

    public override void HandleUpdate()
    {
        prevPressed = pressed;
        pressed = Input.GetKey(keyCode);
    }
}

[Serializable]
public class MouseInputPair : InputPair
{
    public int mouseButton;

    public override void HandleUpdate()
    {
        prevPressed = pressed;
        pressed = Input.GetMouseButton(mouseButton);
    }
}