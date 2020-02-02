using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    //------------------------------------
    //定数
    //------------------------------------
    public const float SHAKE_TIMER = 0.2f;
    public const float SHAKE_AMPLIFIER = 0.2f;

    [SerializeField]
    float mShakeAmplifier;

    [SerializeField]
    float mShakeTimer;

    bool mMinus = false;

    Vector3 rootPosition;

    // Start is called before the first frame update
    void Start()
    {
        rootPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //リクエストがあったらカメラを揺らす
        if (mShakeTimer > 0.0f)
        {
            mShakeTimer -= Time.deltaTime;
            Quaternion rot = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward);
            Vector3 pos = Vector3.up;
            pos = rot * pos;
            float scale = mShakeAmplifier * (mShakeTimer / SHAKE_TIMER);
            if (mMinus)
            {
                scale *= -1.0f;
            }
            mMinus = !mMinus;
            pos = pos * scale;
            pos.z = 0;
            transform.localPosition = rootPosition + pos;

            if (mShakeTimer <= 0.0f)
            {
                transform.localPosition = rootPosition;
            }
        }
    }

    //カメラ揺らしのリクエスト
    public void RequestCameraShake()
    {
        mShakeAmplifier = SHAKE_AMPLIFIER;
        mShakeTimer = SHAKE_TIMER;
    }
}
