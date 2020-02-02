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
    /// <param name="hitPos">Hit Position(hammer)</param>
    /// <param name="result">Great, Good, or Miss</param>
    public void RequestHitEffect(Transform hitPos, HitType result) {
        Transform tr = Instantiate(hitEffect, hitPos).transform;
        Transform spark = tr.GetChild(0);
        if (spark == null)
        {
            Debug.LogError("Spark Effect is not found.");
            return;
        }
        ParticleSystem ps = spark.GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = ps.emission;
        ParticleSystem.Burst burst = new ParticleSystem.Burst(0.0f, 20);
        if (ps == null)
        {
            Debug.LogError("Spark Effect is not a Particle object.");
            return;
        }
        switch (result)
        {
            case HitType.Great:
                tr.localScale = new Vector3(4.0f, 4.0f, 4.0f);
                burst.count = 20;
                break;
            case HitType.OK:
                tr.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                burst.count = 10;
                break;
            case HitType.Miss:
            default:
                tr.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                burst.count = 6;
                break;
        }
        em.SetBurst(0, burst);
        tr.GetComponent<ParticleSystem>().Play();
    }
}
