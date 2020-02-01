using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : SingletonMonoBehaviour<VFXManager>
{
    [SerializeField]
    GameObject hitEffect;   //prefab

    // Start is called before the first frame update
    void Start()
    {
        if (hitEffect == null)
        {
            Debug.LogError("hit Effect prefab is not defined.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This is called when the hammer hits the rail.
    /// </summary>
    /// <param name="rail">Hit rail</param>
    /// <param name="result">Great, Good, or Miss</param>
    public void RequestHitEffect(Transform rail, HitType result)
    {
        GameObject vfx = Instantiate(hitEffect, rail);

        ParticleSystem ps = hitEffect.GetComponent<ParticleSystem>();
        if (ps == null)
        {
            Debug.LogError("Hit Effect is not a Particle object.");
            return;
        }

    }
}
