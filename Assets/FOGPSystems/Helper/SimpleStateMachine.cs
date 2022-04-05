using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FOGPISystems.StateMachine
{


    public class SimpleStateMachine : MonoBehaviour
    {
        [Header("States")]
        public List<SimpleState> States;
        public string StateName;
        protected SimpleState state = null;

        private void SetState(SimpleState s)
        {
            if (s == null)
                return;

            if (state != null)
                state.OnExit();

            state = s;

            state.OnStart();
        }

        public void ChangeState(string stateName)
        {
            foreach(SimpleState _state in States)
            {
                if (stateName.ToLower() == _state.GetType().ToString().ToLower())
                {
                    SetState(_state);
                    Debug.Log("State Changed: " + nameof(_state));
                    StateName = stateName;
                    return;
                }
            }
        }

        private void FixedUpdate()
        {
            if (state == null)
                return;

            state.UpdateState(Time.deltaTime);
        }
    }
}