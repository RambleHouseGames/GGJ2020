using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeTitle : MonoBehaviour
{
    //[SerializeField]
    //ParticleSystem particle;

    [SerializeField]
    UIFlash _pressZKey;

    enum State
    {
        Init,
        WaitKey,
        ToGame
    }

    public string sceneName;

    StateMachine<State> _stateMachine = new StateMachine<State>();

    private void Start()
    {
        SetupState();
    }

    private void Update()
    {
        _stateMachine.UpdateState();
    }

    private void SetupState()
    {
        SetupStateInit();
        SetupStateWaitKey();
        SetupStateToGame();
        _stateMachine.ChangeState(State.Init);
    }

    private void SetupStateInit()
    {
        var state = State.Init;
        var timer = new GameTimer(0.3f);
        Action<State> enter = (prev) =>
        {
            _pressZKey.Stop();
            timer.ResetTimer();

            //SoundManager.Instance.PlayBGM((int)SoundDefine.BGM.Title);
        };
        Action update = () =>
        {
            if (timer.UpdateTimer())
            {
                _stateMachine.ChangeState(State.WaitKey);
            }
        };
        Action<State> exit = (next) => { };
        _stateMachine.AddState(state, enter, update, exit);
    }

    private void SetupStateWaitKey()
    {
        var state = State.WaitKey;
        Action<State> enter = (prev) =>
        {
            _pressZKey.Play();
        };
        Action update = () =>
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _stateMachine.ChangeState(State.ToGame);
            }
        };
        Action<State> exit = (next) => { };
        _stateMachine.AddState(state, enter, update, exit);
    }

    private void SetupStateToGame()
    {
        var state = State.ToGame;
        var timer = new GameTimer(0.5f);
        Action<State> enter = (prev) =>
        {
            //particle.Play();

            timer.ResetTimer();
            _pressZKey.Play(30.0f);

            //SoundManager.Instance.PlaySE((int)SoundDefine.SE.Decide);
        };
        Action update = () =>
        {
            if (timer.UpdateTimer())
            {
                //SoundManager.Instance.StopBGM();
                SceneManager.LoadScene(sceneName);
            }
        };
        Action<State> exit = (next) => { };
        _stateMachine.AddState(state, enter, update, exit);
    }
}
