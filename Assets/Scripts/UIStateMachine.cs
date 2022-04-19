using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FOGPISystems.StateMachine;

public class UIStateMachine : SimpleStateMachine
{
    public UIHUDState uiHUDState;
    public UIPauseState uiPauseState;

    void Start()
    {
        uiHUDState.uiStateMachine = this;
        States.Add(uiHUDState);
        uiPauseState.uiStateMachine = this;
        States.Add(uiPauseState);

        foreach(SimpleState state in States)
        {
            state.StateMachine = this;
        }

        ChangeState(nameof(uiHUDState));
    }

    private void Update()
    {

    }

    public void PauseButton()
    {
        ChangeState(nameof(uiPauseState));
    }

    public void ResumeButton()
    {
        ChangeState(nameof(uiHUDState));
    }
}
