using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer
{
    float _currentTime;
    float _limitTime;

    public bool IsTimeUp
    {
        get
        {
            return _currentTime >= _limitTime;
        }
    }

    public float TimeRate
    {
        get
        {
            return Mathf.Min(1.0f, _currentTime / _limitTime);
        }
    }

    public GameTimer(float time)
    {
        _limitTime = time;
    }

    public void ResetTimer()
    {
        _currentTime = 0.0f;
    }

    public bool UpdateTimer(float scale = 1.0f)
    {
        _currentTime += Time.deltaTime * scale;
        return IsTimeUp;
    }
}
