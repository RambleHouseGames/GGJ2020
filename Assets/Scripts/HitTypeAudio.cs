using UnityEngine;

public class HitTypeAudio : MonoBehaviour {
    public AudioSource hitSourceLeft;
    public AudioSource hitSourceRight;
    public AudioClip greatHitClip;
    public AudioClip okHitClip;
    public AudioClip missHitClip;

    public void PlayHitSound(HitType hitType, bool left) {
        AudioSource hitSource = left ? hitSourceLeft : hitSourceRight;
        AudioClip hitClip;
        switch (hitType) {
            case HitType.Great:
                hitClip = greatHitClip;
                break;
            case HitType.OK:
                hitClip = okHitClip;
                break;
            case HitType.Miss:
                hitClip = missHitClip;
                break;
            default:
                hitClip = missHitClip;
                break;
        }
        hitSource.PlayOneShot(hitClip);
    }
}
