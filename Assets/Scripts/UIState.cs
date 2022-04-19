using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FOGPISystems.StateMachine;

[System.Serializable]
public class UIState : SimpleState
{
    [HideInInspector]
    public UIStateMachine uiStateMachine;
}
