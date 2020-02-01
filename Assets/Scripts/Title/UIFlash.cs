using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlash : MonoBehaviour
{
    private enum State
    {
        None,
        Flash,
    }

    [SerializeField]
    Text _text = null;

    Color _initColor;

    StateMachine<State> _stateMachine = new StateMachine<State>();

    float _flashScale = 1.0f;

    void Awake()
    {
        _initColor = _text.color;
        SetupState();
    }

    void Update()
    {
        _stateMachine.UpdateState();
    }

    public void Play(float flashScale = 1.0f)
    {
        _flashScale = flashScale;
        _stateMachine.ChangeState(State.Flash);
    }

    public void Stop()
    {
        _stateMachine.ChangeState(State.None);
    }

    private void SetupState()
    {
        SetupStateNone();
        SetupStateFlash();
        _stateMachine.ChangeState(State.None);
    }

    private void SetupStateNone()
    {
        var state = State.None;
        Action<State> enter = (prev) => { };
        Action update = () => { };
        Action<State> exit = (next) => { };
        _stateMachine.AddState(state, enter, update, exit);
    }

    private void SetupStateFlash()
    {
        var state = State.Flash;
        Action<State> enter = (prev) =>
        {
        };
        Action update = () =>
        {
            var color = _initColor;
            color.a = Mathf.Abs(Mathf.Sin(Time.time * _flashScale));
            _text.color = color;
        };
        Action<State> exit = (next) =>
        {
            _text.color = _initColor;
        };
        _stateMachine.AddState(state, enter, update, exit);
    }


}
