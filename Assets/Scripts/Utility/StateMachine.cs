using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    private class State
    {
        public T StateValue { get; private set; } = default(T);
        public Action<T> Enter { get; private set; } = (prev) => { };
        public Action Update { get; private set; } = () => { };
        public Action<T> Exit { get; private set; } = (next) => { };
        public State(T state, Action<T> enter, Action update, Action<T> exit)
        {
            StateValue = state;
            Enter = enter;
            Update = update;
            Exit = exit;
        }
    }

    Dictionary<T, State> _StateDict = new Dictionary<T, State>();

    State _CurrentState = null;

    public void AddState(T state, Action<T> enter, Action update, Action<T> exit)
    {
        if (_StateDict.ContainsKey(state))
        {
            Debug.LogError(string.Format("すでに{0}のステートが追加されています。", state));
            return;
        }
        var instance = new State(state, enter, update, exit);
        _StateDict.Add(state, instance);
    }

    public void ChangeState(T next)
    {
        // 終了処理を呼ぶ
        if (_CurrentState != null)
        {
            _CurrentState.Exit(next);
        }

        // なければエラー
        if (_StateDict.ContainsKey(next) == false)
        {
            Debug.LogError(string.Format("登録されていないステートに変更しようとしています。", next));
            return;
        }

        // ステート更新
        var prev = _CurrentState == null ? default(T) : _CurrentState.StateValue;
        _CurrentState = _StateDict[next];

        // 開始処理を呼ぶ
        _CurrentState.Enter(prev);
    }

    public void UpdateState()
    {
        if (_CurrentState == null)
            return;

        _CurrentState.Update();
    }
}
