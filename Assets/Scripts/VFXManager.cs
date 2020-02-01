using UnityEngine;

public class VFXManager : SingletonMonoBehaviour<VFXManager> {
    [SerializeField]
    GameObject hitEffect;   //prefab

    void Start() {
        if (hitEffect == null) {
            Debug.LogError("hit Effect prefab is not defined.");
        }
    }

    /// <summary>
    /// This is called when the hammer hits the rail.
    /// </summary>
    /// <param name="railPos">Hit rail</param>
    /// <param name="result">Great, Good, or Miss</param>
    public void RequestHitEffect(Transform railPos, HitType result) {
        Instantiate(hitEffect, railPos);

        ParticleSystem ps = hitEffect.GetComponent<ParticleSystem>();
        if (ps == null) {
            Debug.LogError("Hit Effect is not a Particle object.");
            return;
        }

    }
}
